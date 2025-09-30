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
            var user = httpContext.User;
            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var appUser = userManager.FindById(user.Identity.GetUserId());
            var session = httpContext.Session;
            if (!user.Identity.IsAuthenticated)
                return false;
            if (!appUser.TwoFactorEnabled)
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