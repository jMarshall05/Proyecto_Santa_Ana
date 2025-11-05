using System;
using System.Configuration;
using System.Linq;
using System.Runtime.Caching;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Campus.Abstracciones.LogicaDeNegocio;
using Campus.Abstracciones.LogicaDeNegocio.Telefonos.AgregarTelefono;
using Campus.Abstracciones.LogicaDeNegocio.Usuarios.AgregarUsuariosLN;
using Campus.Abstracciones.ModelosUI;
using Campus.LogicaDeNegocio.Bitacora;
using Campus.LogicaDeNegocio.Telefonos.AgregarTelefonoLN;
using Campus.LogicaDeNegocio.Usuarios.AgregarUsuarios;
using Campus.UI.Helpers;
using Campus.UI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using OtpNet;
using QRCoder;

namespace Campus.UI.Controllers
{
    //[Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private readonly IAgregarUsuariosLN _agregarUsuariosLN;
        private readonly Random rnd;
        private readonly IAgregarTelefonoLN _agregarTelefonoLN;
        private readonly IBitacoraLN _bitacoraLN;

        private static byte[] qrCodeImage;

        public AccountController()
        {
            _agregarUsuariosLN = new AgregarUsuariosLN();
            rnd = new Random();
            _agregarTelefonoLN = new AgregarTelefonoLN();
            _bitacoraLN = new BitacoraLN();
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = UserManager.FindByEmail(model.Email);
            if (user != null)
            {
                var result = await SignInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, shouldLockout: false);
                switch (result)
                {
                    case SignInStatus.Success:
                        if (user.TwoFactorEnabled && !string.IsNullOrEmpty(user.GoogleAuthenticatorSecretKey))
                        {
                            Session["UserIdFor2FA"] = user.Id;
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            var cache = MemoryCache.Default;
                            var verifiedKey = $"User2FAVerified-{user.Id}";

                            if (cache[verifiedKey] as bool? == true)
                                return RedirectToAction("Index", "Home");

                            return RedirectToAction("Solicitud2FA", "Account");
                        }
                    case SignInStatus.LockedOut:
                        return View("Lockout");
                    case SignInStatus.RequiresVerification:
                        return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, model.RememberMe });
                    case SignInStatus.Failure:
                    default:
                        ModelState.AddModelError("", "Intento de inicio de sesión a fallado.");
                        return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("", "Intento de inicio de sesión a fallado.");
                return View(model);
            }
        }


        public ActionResult LoginWith2FA()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginWith2FA(string code, bool rememberMe = false)
        {
            var userId = Session["UserIdFor2FA"]?.ToString();
            if (userId == null) return RedirectToAction("Login");

            var user = UserManager.FindById(userId);
            var totp = new Totp((Base32Encoding.ToBytes(Encriptacion.Desencriptar(user.GoogleAuthenticatorSecretKey))));
            if (totp.VerifyTotp(code, out _, VerificationWindow.RfcSpecifiedNetworkDelay))
            {
                SignInManager.SignIn(user, isPersistent: rememberMe, rememberBrowser: false);
                Session.Remove("UserIdFor2FA");
                var cache = MemoryCache.Default;
                var verifiedKey = $"User2FAVerified-{user.Id}";
                cache.Add(verifiedKey, true, DateTimeOffset.Now.AddMinutes(30));
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Código inválido.");
                return View();
            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Requerir que el usuario haya iniciado sesión con nombre de usuario y contraseña o inicio de sesión externo
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // El código siguiente protege de los ataques por fuerza bruta a los códigos de dos factores. 
            // Si un usuario introduce códigos incorrectos durante un intervalo especificado de tiempo, la cuenta del usuario 
            // se bloqueará durante un período de tiempo especificado. 
            // Puede configurar el bloqueo de la cuenta en IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Código no válido.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ApplicationUser user = CrearUsuario(model);
                    var result = await UserManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        await UserManager.AddToRoleAsync(user.Id, model.Rol);
                        var usuario = ConvertirDto(model, user);
                        await _agregarUsuariosLN.AgregarUsuario(usuario);
                        model.Telefonos.ForEach(t => t.IdUsuario = user.Id);
                        await _agregarTelefonoLN.AgregarTelefono(model.Telefonos);
                        var bitacora = new BitacoraDto
                        {
                            Fecha = DateTime.Now,
                            Usuario = user.Id,
                            Accion = "INSERT",
                            Tabla = "AspNetUsers",
                            Descripcion = $"Registro de nuevo usuario - Email: {model.Email}, Nombre: {model.Nombre} {model.Apellido}, Rol: {model.Rol}, Cédula: {model.Cedula}"
                        };
                        _bitacoraLN.RegistrarEvento(bitacora);

                        // await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                        // Para obtener más información sobre cómo habilitar la confirmación de cuentas y el restablecimiento de contraseña, visite https://go.microsoft.com/fwlink/?LinkID=320771
                        // Enviar un correo electrónico con este vínculo
                        // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        // await UserManager.SendEmailAsync(user.Id, "Confirmar la cuenta", "Para confirmar su cuenta, haga clic <a href=\"" + callbackUrl + "\">aquí</a>");

                        return RedirectToAction("ListarUsuarios", "Usuarios");
                    }
                    AddErrors(result);
                }

                // Si llegamos a este punto, es que se ha producido un error y volvemos a mostrar el formulario
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        private ApplicationUser CrearUsuario(RegisterViewModel model)
        {
            string numeroRamdon = rnd.Next(0, 100).ToString("D2");
            var user = new ApplicationUser { UserName = model.Nombre.ToUpper().First() + model.Apellido.Trim() + numeroRamdon, Email = model.Email };
            return user;
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    // No revelar que el usuario no existe o que no está confirmado
                    return View("ForgotPasswordConfirmation");
                }

                // Para obtener más información sobre cómo habilitar la confirmación de cuentas y el restablecimiento de contraseña, visite https://go.microsoft.com/fwlink/?LinkID=320771
                // Enviar un correo electrónico con este vínculo
                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code }, protocol: Request.Url.Scheme);
                await UserManager.SendEmailAsync(user.Id, "Restablecer contraseña", "Para restablecer la contraseña, haga clic <a href=\"" + callbackUrl + "\">aquí</a>");
                return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // Si llegamos a este punto, es que se ha producido un error y volvemos a mostrar el formulario
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string userId, string code)
        {
            if (code == null || userId == null)
                return View("Error");

            // Crear el modelo con los datos necesarios
            var model = new ResetPasswordViewModel
            {
                Code = code,
                Id = userId,
            };

            return View(model);
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                // Bitácora: restablecimiento de contraseña
                var bitacora = new BitacoraDto
                {
                    Fecha = DateTime.Now,
                    Usuario = user.Id,
                    Accion = "UPDATE",
                    Tabla = "AspNetUsers",
                    Descripcion = $"Restablecimiento de contraseña - Usuario: {user.Email}"
                };
                _bitacoraLN.RegistrarEvento(bitacora);

                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Solicitar redireccionamiento al proveedor de inicio de sesión externo
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generar el token y enviarlo
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, model.ReturnUrl, model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Si el usuario ya tiene un inicio de sesión, iniciar sesión del usuario con este proveedor de inicio de sesión externo
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // Si el usuario no tiene ninguna cuenta, solicitar que cree una
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Obtener datos del usuario del proveedor de inicio de sesión externo
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "Account");
        }
        public ActionResult EnableAuthenticator()
        {
            var userId = User.Identity.GetUserId();
            var user = UserManager.FindById(userId);

            // Generar clave secreta si no existe
            if (string.IsNullOrEmpty(user.GoogleAuthenticatorSecretKey))
            {
                var secretKey = Base32Encoding.ToString(KeyGeneration.GenerateRandomKey(20));
                var EncryptedKey = Encriptacion.Encriptar(secretKey);
                user.GoogleAuthenticatorSecretKey = EncryptedKey;
                user.TwoFactorEnabled = true;
                UserManager.Update(user);

                var bitacora = new BitacoraDto
                {
                    Fecha = DateTime.Now,
                    Usuario = userId,
                    Accion = "UPDATE",
                    Tabla = "AspNetUsers",
                    Descripcion = "Configuración inicial de autenticador Google (2FA)"
                };
                _bitacoraLN.RegistrarEvento(bitacora);
            }

            user.GoogleAuthenticatorSecretKey = Encriptacion.Desencriptar(user.GoogleAuthenticatorSecretKey);
            string issuer = ConfigurationManager.AppSettings["FromName"];
            string otpauthUrl = $"otpauth://totp/{issuer}:{user.Email}?secret={user.GoogleAuthenticatorSecretKey}&issuer={issuer}";

            using (var qrGenerator = new QRCodeGenerator())
            using (var qrCodeData = qrGenerator.CreateQrCode(otpauthUrl, QRCodeGenerator.ECCLevel.Q))
            using (var qrCode = new PngByteQRCode(qrCodeData))
            {
                qrCodeImage = qrCode.GetGraphic(20);
                ViewBag.QRCode = "data:image/png;base64," + Convert.ToBase64String(qrCodeImage);
            }

            ViewBag.SecretKey = user.GoogleAuthenticatorSecretKey;
            return View();
        }
        public ActionResult DisableAuthenticator()
        {
            var id = User.Identity.GetUserId();
            var user = UserManager.FindById(id);
            if (user != null)
            {
                user.TwoFactorEnabled = false;
                user.GoogleAuthenticatorSecretKey = null;
                UserManager.Update(user);
                var cache = MemoryCache.Default;
                var User2FA = $"User2FA-{user.Id}";
                if (cache[User2FA] != null)
                    cache.Remove(User2FA);
                cache.Add(User2FA, false, DateTimeOffset.Now.AddMinutes(30));

                // Bitácora: desactivación de autenticador
                var bitacora = new BitacoraDto
                {
                    Fecha = DateTime.Now,
                    Usuario = id,
                    Accion = "UPDATE",
                    Tabla = "AspNetUsers",
                    Descripcion = "Desactivación de autenticador Google (2FA)"
                };
                _bitacoraLN.RegistrarEvento(bitacora);

                return RedirectToAction("Index", "Manage");
            }
            else
            {
                return RedirectToAction("Index", "Manage");
            }
        }
        [HttpPost]
        public ActionResult VerifyAuthenticator(string code)
        {
            var userId = User.Identity.GetUserId();
            var user = UserManager.FindById(userId);

            var totp = new Totp(Base32Encoding.ToBytes(Encriptacion.Desencriptar(user.GoogleAuthenticatorSecretKey)));

            if (totp.VerifyTotp(code, out long _, VerificationWindow.RfcSpecifiedNetworkDelay))
            {
                user.TwoFactorEnabled = true;
                UserManager.Update(user);

                var cache = MemoryCache.Default;
                var verifiedKey = $"User2FAVerified-{user.Id}";
                var User2FA = $"User2FA-{user.Id}";
                if (cache[verifiedKey] != null)
                    cache.Remove(verifiedKey);
                cache.Add(verifiedKey, true, DateTimeOffset.Now.AddMinutes(30));
                if (cache[User2FA] != null)
                    cache.Remove(User2FA);
                cache.Add(User2FA, true, DateTimeOffset.Now.AddMinutes(30));

                // Bitácora: verificación exitosa de autenticador
                var bitacora = new BitacoraDto
                {
                    Fecha = DateTime.Now,
                    Usuario = userId,
                    Accion = "UPDATE",
                    Tabla = "AspNetUsers",
                    Descripcion = "Verificación exitosa de autenticador Google (2FA) - 2FA activado"
                };
                _bitacoraLN.RegistrarEvento(bitacora);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Código inválido.");
                ViewBag.QRCode = "data:image/png;base64," + Convert.ToBase64String(qrCodeImage);
                return View("EnableAuthenticator");
            }
        }

        [HttpGet]
        public ActionResult Solicitud2FA()
        {
            return View();
        }
        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        private UsuariosDto ConvertirDto(RegisterViewModel model, ApplicationUser user)
        {

            string rol = UserManager.GetRoles(user.Id).FirstOrDefault();
            return new UsuariosDto
            {
                IdUsuario = user.Id,
                Nombre = model.Nombre,
                Apellido = model.Apellido,
                Email = model.Email,
                FechaDeNacimiento = model.FechaDeNacimiento,
                Cedula = model.Cedula,
                FechaDeRegistro = DateTime.Now,
                Rol = rol, // Asignar un rol predeterminado
                Estado = true // Asignar estado activo por defecto
            };
        }

        #region Aplicaciones auxiliares
        // Se usa para la protección XSRF al agregar inicios de sesión externos
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}