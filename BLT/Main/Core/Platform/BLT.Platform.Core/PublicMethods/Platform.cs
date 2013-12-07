using BLT.Core;
using BLT.Core.Logging.PlatformLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLT.Platform.Core
{
    public static class Platform
    {
        public static bool Verify()
        {
            if (Sql.Statements.VerificationStatements.DatabaseExists("Platform", EnvironmentSettings.DatabaseConnections.MasterSqlConnection))
            {
                return true;
            }
            else
            {
                PlatformLog.LogActivity(ProvisioningLogActivity.PlatformProvisioningStarted, "Platform Provisioning Started on Environment: " + EnvironmentSettings.Environment.Current.ToUpper());

                Sql.Statements.ManagementStatements.CreateDatabase("Platform", EnvironmentSettings.DatabaseConnections.MasterSqlConnection);
                Sql.Statements.ManagementStatements.RunSqlScript("Sample_Create.sql", EnvironmentSettings.DatabaseConnections.PlatformSqlConnection);
                Sql.Statements.ManagementStatements.RunSqlScript("Sample_Seed.sql", EnvironmentSettings.DatabaseConnections.PlatformSqlConnection);

                PlatformLog.LogActivity(ProvisioningLogActivity.PlatformProvisioningCompleted, "Platform Provisioning Completed on Environment: " + EnvironmentSettings.Environment.Current.ToUpper());

                return true;
            }
        }
    }
}
