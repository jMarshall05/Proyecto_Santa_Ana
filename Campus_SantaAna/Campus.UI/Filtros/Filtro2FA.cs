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

            if (path.Contains("/account/loginwith2fa") ||
                path.Contains("/account/login") ||
                path.Contains("/account/register") ||
                path.Contains("/account/forgotpassword") ||
                path.Contains("/account/solicitud2fa"))

            {
                return true;
            }

            var user = httpContext.User;
            var session = httpContext.Session;

            if (!user.Identity.IsAuthenticated)
                return false;

            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var userId = user.Identity.GetUserId();
            var cacheKey = $"User2FA-{userId}";
            var cache = MemoryCache.Default;

            var is2FAEnabled = cache[cacheKey] as bool?;

            if (is2FAEnabled == null)
            {
                var appUser = userManager.FindById(userId);

                is2FAEnabled = appUser.TwoFactorEnabled;

                cache.Add(cacheKey, is2FAEnabled, DateTimeOffset.Now.AddMinutes(15));
            }
            if (!is2FAEnabled.Value)
                return true;

            if (session["UserIdFor2FA"] != null)
                return false;

            return true;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            // Redirige al flujo de 2FA
            filterContext.Result = new RedirectResult("/Account/LoginWith2FA");
        }

    }
}