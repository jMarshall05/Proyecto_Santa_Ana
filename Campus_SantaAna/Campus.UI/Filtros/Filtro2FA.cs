using System;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using HttpContext = System.Web.HttpContext;

namespace Campus.UI.Filtros
{
    public class Filtro2FA : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var path = httpContext.Request.Path.ToLower();

            // Permitir acceso libre a las rutas públicas
            if (path == "/account/login" ||
                path == "/account/register" ||
                path == "/account/forgotpassword" ||
                path == "/account/enableauthenticator" ||
                path == "/account/logoff" ||
                path == "/account/loginwith2fa")
            {
                return true;
            }

            var user = httpContext.User;
            var session = httpContext.Session;

            if (!user.Identity.IsAuthenticated)
                return false;

            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var userId = user.Identity.GetUserId();
            var cache = MemoryCache.Default;

            var twoFAKey = $"User2FA-{userId}";
            var verifiedKey = $"User2FAVerified-{userId}";

            var is2FAEnabled = cache[twoFAKey] as bool?;

            if (is2FAEnabled == null)
            {
                var appUser = userManager.FindById(userId);
                is2FAEnabled = appUser.TwoFactorEnabled;
                cache.Add(twoFAKey, is2FAEnabled, DateTimeOffset.Now.AddMinutes(30));
            }

            if (!is2FAEnabled.Value)
                return true;

            var isRecentlyVerified = cache[verifiedKey] as bool?;
            if (isRecentlyVerified == true)
                return true;

            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("/Account/LoginWith2FA");
        }
    }
}
