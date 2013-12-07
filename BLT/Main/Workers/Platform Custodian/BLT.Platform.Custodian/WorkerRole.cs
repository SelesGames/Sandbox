using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using BLT.Core.Logging.PlatformLogs;
using BLT.Core;

namespace BLT.Platform.Custodian
{
    public class WorkerRole : RoleEntryPoint
    {
        public override void Run()
        {
            // This is a sample worker implementation. Replace with your logic.
            Trace.TraceInformation("Platform Custodian Worker: entry point called", "Information");

            PlatformLog.LogActivity(CustodianLogActivity.ScheduledTask,
                    "Custodian started on environment: " + EnvironmentSettings.Environment.Current.ToUpper());

            while (true)
            {
                PlatformLog.LogActivity(CustodianLogActivity.ScheduledTask,
                    "Tasks started...");

                //  TASK 1:   Purge Unverified Accounts:
                /////Purge.PurgeUnverifiedAccounts(EnvironmentSettings.DatabaseConnections.PlatformSqlConnection, CustodianSettings.UnverifiedAccounts.ExpirationDays);


                //  TASK 2:   Cleanup Source Images:

                //  TASK 3:   Check for lapsed accounts, update account and send warnings or purge if lapse is over grace period
                /////Check.CheckForLapsedAccounts(EnvironmentSettings.DatabaseConnections.AccountsSqlConnection, CustodianSettings.LapsedAccounts.WarningPeriodDays, CustodianSettings.LapsedAccounts.GracePeriodDays);


                Trace.TraceInformation("Tasks complete.");
                Trace.TraceInformation("Custodian sleeping for: " + CustodianSettings.Frequency.Description);

                //Log
                PlatformLog.LogActivity(CustodianLogActivity.ScheduledTask,
                    "Tasks complete, sleeping for: " + CustodianSettings.Frequency.Description);

                //Sleep
                Thread.Sleep(CustodianSettings.Frequency.Length);

            }
        }

        public override bool OnStart()
        {
            BLT.Core.EnvironmentSettings.Initialize();

            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

            return base.OnStart();
        }
    }
}
