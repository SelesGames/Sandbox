using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BLT.ClientExtranet.Web.GroupPortal.Startup))]
namespace BLT.ClientExtranet.Web.GroupPortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
