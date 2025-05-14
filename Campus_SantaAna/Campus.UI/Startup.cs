using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Campus.UI.Startup))]
namespace Campus.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
