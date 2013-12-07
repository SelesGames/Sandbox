using BLT.Core;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BLT.ClientExtranet.Web.GroupPortal
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //EnvironmentSettings.Initialize();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
