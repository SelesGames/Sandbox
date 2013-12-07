using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace BLT.Platform.Custodian
{

    /// <summary>
    /// A static helper class to access configuration constants that do not change across deployment environments
    /// </summary>
    public static class CustodianSettings
    {

        /*
        public const int Setting1 = 0; 
        public const string Setting2 = "";
        */

        /// <summary>
        /// The amount of time for worker thread to sleep between custodial duties (180000 = 3 minutes)
        /// </summary>
        public static class Frequency
        {
            //public const int Length = 43200000; //<--- Milliseconds
            //public const string Description = "12 Hours";

            public const int Length = 180000; //<--- Milliseconds
            public const string Description = "3 Minutes";
        }

        public static class UnverifiedAccounts
        {
            public const int ExpirationDays = 20; //<--- Number of days to allow unverified accounts to sit idle in the system before purging
        }

        public static class LapsedAccounts
        {
            public const int WarningPeriodDays = 1; //<--- Number of days to allow an account to lapse before sending a warning to the user 
            public const int GracePeriodDays = 15; //<--- Number of days to wait before purging a lapsed account
        }
    }
}