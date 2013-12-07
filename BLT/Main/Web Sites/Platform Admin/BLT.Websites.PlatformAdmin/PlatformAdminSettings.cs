using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace BLT.Websites.PlatformAdmin
{

    /// <summary>
    /// A static helper class to access configuration constants that do not change across deployment environments
    /// </summary>
    public static class PlatformAdminSettings
    {

        public const string Setting0 = "Setting Value 0";
        public const string Setting1 = "Setting Value 1";

        public static class ParentSetting
        {
            public const string ChildSetting0 = "Child Setting Value 0";
            public const string ChildSetting1 = "Child Setting Value 1";
            public const string ChildSetting2 = "Child Setting Value 2";
        }

    }
}