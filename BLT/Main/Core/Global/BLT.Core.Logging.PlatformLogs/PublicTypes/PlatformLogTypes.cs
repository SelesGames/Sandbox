using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLT.Core.Logging.PlatformLogs
{
    #region Return Object(s)

    /// <summary>
    /// A generic representation of an internal log entity for public consumption. used as a return type for log retreival methods 
    /// </summary>
    public class PlatformLogTableEntity : TableEntity
    {
        public PlatformLogTableEntity()
        {

        }

        public string ActivityType { get; set; }
        public string Description { get; set; }
        public string Details { get; set; }

    }

    #endregion

    #region Enums

    /// <summary>
    /// A record of each Master log available
    /// Used to: Loop Through PlatformLogTypeNames when clearing logs
    /// </summary>
    public enum PlatformLogName
    {
        CustodianLog,
        RegistrationLog,
        ErrorLog,
        ProvisioningLog,
        ApplicationLog,
        GroupActivity
    }

    /// <summary>
    /// PartitionTypes for logs
    /// /// Used to: Generate the trailing LogName for classes derived from AccountLogType when creating sorted partitions within the private method for: WriteAccountLog()
    /// Used to: Distinguish the desired partition types when retriveing logs with public method: PlatformLog.GetLogByPartition()
    /// Used to: Loop Through PlatformLogPartition when clearing logs
    /// </summary>
    public enum PlatformLogPartition
    {
        Activity,
        Time
    }

    #endregion

    #region Log Types

    #region LogType Interface & Base Class

    public interface IPlatformLogType
    {
        string LogName { get; set; }
        string Activity { get; set; }

    }

    public abstract class PlatformLogType : IPlatformLogType
    {
        public string Activity { get; set; }
        public string LogName { get; set; }
    }

    #endregion

    #region MasterLog ActivityTypes

    #region CUSTODIAN Log Types

    public class CustodianLog : PlatformLogType
    {
        public CustodianLog(string activity)
        {
            this.LogName = PlatformLogName.CustodianLog.ToString().ToLower();
            this.Activity = activity;
        }

    }

    public static class CustodianLogActivity
    {

        public static CustodianLog ScheduledTask = new CustodianLog("scheduled-task");

        //public static CustodianLog UnverifiedAccountsPurged = new CustodianLog("unverified-accounts-purged");
        public static CustodianLog UnverifiedAccountPurged = new CustodianLog("unverified-account-purged");

        public static CustodianLog LapsedAccountWarned = new CustodianLog("lapsed-account-warned");
        public static CustodianLog LapsedAccountPurged = new CustodianLog("lapsed-account-purged");

    }


    #endregion

    #region GroupActivity Log Types

    public class GroupActivity : PlatformLogType
    {
        public GroupActivity(string activity)
        {
            this.LogName = PlatformLogName.GroupActivity.ToString().ToLower();
            this.Activity = activity;
        }

    }

    public static class GroupLogActivity
    {

        public static CustodianLog RoundViewed = new CustodianLog("round-viewed");


    }


    #endregion


    #region REGISTRATION Log Types

    public class RegistrationLog : PlatformLogType
    {
        public RegistrationLog(string activity)
        {
            this.LogName = PlatformLogName.RegistrationLog.ToString().ToLower();
            this.Activity = activity;
        }
    }

    public static class RegistrationLogActivity
    {
        public static RegistrationLog RegistrationAttempted = new RegistrationLog("registration-attempted");
        public static RegistrationLog RegistrationFailed = new RegistrationLog("registration-failed");
        public static RegistrationLog RegistrationComplete = new RegistrationLog("registration-complete");
        public static RegistrationLog AccountVerified = new RegistrationLog("account-verified");
    }


    #endregion

    #region ERROR Log Types

    public class ErrorLog : PlatformLogType
    {
        public ErrorLog(string activity)
        {
            this.LogName = PlatformLogName.ErrorLog.ToString().ToLower();
            this.Activity = activity;
        }
    }

    public static class ErrorLogActivity
    {
        public static ErrorLog PlatformError = new ErrorLog("platform-error");
        public static ErrorLog PlatformWorkerError = new ErrorLog("platform-worker-error");
        public static ErrorLog PlatformServicesError = new ErrorLog("platform-services-error");
    }


    #endregion

    #region PROVISIONING Log Types

    public class ProvisioningLog : PlatformLogType
    {
        public ProvisioningLog(string activity)
        {
            this.LogName = PlatformLogName.ProvisioningLog.ToString().ToLower();
            this.Activity = activity;
        }
    }

    public static class ProvisioningLogActivity
    {
        public static ProvisioningLog AccountProvisioningStarted = new ProvisioningLog("account-provisioning-started");
        public static ProvisioningLog AccountProvisioningCompleted = new ProvisioningLog("account-provisioning-completed");

        public static ProvisioningLog PlatformProvisioningStarted = new ProvisioningLog("platform-provisioning-started");
        public static ProvisioningLog PlatformProvisioningCompleted = new ProvisioningLog("platform-provisioning-completed");
    }


    #endregion

    #region STARTUP Log Types

    public class ApplicationLog : PlatformLogType
    {
        public ApplicationLog(string activity)
        {
            this.LogName = PlatformLogName.ApplicationLog.ToString().ToLower();
            this.Activity = activity;
        }
    }

    public static class ApplicationLogActivity
    {
        public static ApplicationLog ApplicationStarted = new ApplicationLog("application-started");
        public static ApplicationLog ApplicationVerified = new ApplicationLog("application-verified");
    }


    #endregion

    #endregion

    #endregion

}
