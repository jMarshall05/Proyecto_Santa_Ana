using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Campus.UI.Models;
using System.Net.Mail;
using System.Net;

namespace Campus.UI
{
    public class EmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("proyecto.santaana123@gmail.com", "qrws xtjf nziv mdoa"),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            // Leer el template HTML
            string templatePath = HttpContext.Current.Server.MapPath("~/Views/Account/ResetPasswordEmail.cshtml");
            string emailTemplate = System.IO.File.ReadAllText(templatePath);

            // Extraer la URL del mensaje original
            string resetUrl = ExtractUrlFromMessage(message.Body);
            string id = ExtractId(resetUrl);

            // Reemplazar placeholders
            emailTemplate = emailTemplate.Replace("{RESET_URL}", resetUrl);
            emailTemplate = emailTemplate.Replace("{APP_NAME}", "Santa Ana a Un Click");
            emailTemplate = emailTemplate.Replace("{USER_ID}", id);


            // Crear el mensaje
            var mailMessage = new MailMessage
            {
                From = new MailAddress("proyecto.santaana123@gmail.com", "✏️ Santa Ana a Un Click"),
                Subject = "🔑 Restablecimiento de Contraseña - Acción Requerida",
                Body = emailTemplate,
                IsBodyHtml = true
            };

            mailMessage.To.Add(message.Destination);

            try
            {
                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                // Log del error
                System.Diagnostics.Debug.WriteLine($"Error enviando email: {ex.Message}");
                throw; // Re-lanzar para que Identity maneje el error
            }
            finally
            {
                mailMessage.Dispose();
                smtpClient.Dispose();
            }
        }
        private string ExtractUrlFromMessage(string originalMessage)
        {
            try
            {
                int startIndex = originalMessage.IndexOf("href=\"") + 6;
                int endIndex = originalMessage.IndexOf("\"", startIndex);
                return originalMessage.Substring(startIndex, endIndex - startIndex);
            }
            catch
            {
                return "#"; // URL por defecto si hay error
            }
        }
        private string ExtractId(string resetUrl)
        {
            try
            {
                int startIndex = resetUrl.IndexOf("userId=") + 7;
                int endIndex = resetUrl.IndexOf("&code", startIndex);
                return resetUrl.Substring(startIndex, endIndex - startIndex);

            }

            catch
            {
                return "#"; // URL por defecto si hay error
            }
        }
    }


    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Conecte el servicio SMS aquí para enviar un mensaje de texto.
            return Task.FromResult(0);
        }
    }

    // Configure el administrador de usuarios de aplicación que se usa en esta aplicación. UserManager se define en ASP.NET Identity y se usa en la aplicación.
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure la lógica de validación de nombres de usuario
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure la lógica de validación de contraseñas
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configurar valores predeterminados para bloqueo de usuario
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Registre los proveedores de autenticación de dos factores. Esta aplicación usa el teléfono y el correo electrónico para recibir un código de verificación del usuario
            // Puede escribir su propio proveedor y conectarlo aquí.
            manager.RegisterTwoFactorProvider("Código telefónico", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Su código de seguridad es {0}"
            });
            manager.RegisterTwoFactorProvider("Código de correo electrónico", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Código de seguridad",
                BodyFormat = "Su código de seguridad es {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    // Configure el administrador de inicios de sesión que se usa en esta aplicación.
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}
