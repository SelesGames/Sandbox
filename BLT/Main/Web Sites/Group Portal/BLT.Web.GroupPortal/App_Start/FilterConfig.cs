using System.Web;
using System.Web.Mvc;

namespace BLT.ClientExtranet.Web.GroupPortal
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
