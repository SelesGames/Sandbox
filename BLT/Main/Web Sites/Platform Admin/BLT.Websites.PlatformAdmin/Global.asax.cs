using BLT.Core;
using BLT.Core.Logging.PlatformLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BLT.Websites.PlatformAdmin
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //Generate Environment Settings:
            BLT.Core.EnvironmentSettings.Initialize();

            //Log Actions:
            PlatformLog.LogActivity(ApplicationLogActivity.ApplicationStarted, "Platform Admin started on environment: " + EnvironmentSettings.Environment.Current.ToUpper());

            //Verify:
            BLT.Platform.Core.Platform.Verify();

            //Log Actions:
            PlatformLog.LogActivity(ApplicationLogActivity.ApplicationVerified, "Platform Admin verified on environment: " + EnvironmentSettings.Environment.Current.ToUpper());



            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
