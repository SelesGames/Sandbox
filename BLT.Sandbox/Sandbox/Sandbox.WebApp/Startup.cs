using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Sandbox.WebApp.Startup))]
namespace Sandbox.WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
