using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLT.Core.Logging.PlatformLogs
{
    public static class PlatformLog
    {
        #region Log Methods

        public static void LogActivity(PlatformLogType activity, string description, string details = null)
        {

            #region Fire & Forget on a background thread (More overhead)
            /*
            System.Threading.ThreadStart threadStart = delegate
            {
                //Log the activity
                PrivateMethods.WritePlatformLog(activity, description, details);

            };
            System.Threading.Thread thread = new System.Threading.Thread(threadStart);
            thread.IsBackground = true;
            thread.Start();
            */
            #endregion

            #region Fire & Forget using ThreadPool.QueueUserWorkItem (Pool of threads at the ready)

            ThreadPool.QueueUserWorkItem(o => PrivateMethods.WritePlatformLog(activity, description, details));
            #endregion

        }

        #endregion

        #region Get Methods

        #region Get Logs By Time

        /// <summary>
        /// Returns all activities in a log, newest records first
        /// </summary>
        /// <param name="masterLog"></param>
        /// <returns></returns>
        public static IEnumerable<PlatformLogTableEntity> GetLog(PlatformLogName platformLogName, int maxRecords)
        {
            return PrivateMethods.GetFullLogByTime(platformLogName, maxRecords);
        }

        #endregion

        #region Get Activity Specific Records From a Log

        /// <summary>
        /// Returns all records of a particular activity in a log, newest records first
        /// </summary>
        /// <param name="activity"></param>
        /// <param name="maxRecords"></param>
        /// <returns></returns>
        public static IEnumerable<PlatformLogTableEntity> GetLog(PlatformLogType activity, int maxRecords)
        {
            return PrivateMethods.GetActivityLogByTime(activity, maxRecords);
        }

        #endregion

        #endregion

        #region Clear Methods

        /// <summary>
        /// Clear all log tables/data
        /// </summary>
        public static bool ClearLogs()
        {
            return PrivateMethods.ClearLogs();
        }

        /// <summary>
        /// Clear log tables/data for a particular LogType
        /// </summary>
        public static bool ClearLog(PlatformLogName platformLogTypeName)
        {
            return PrivateMethods.ClearLog(platformLogTypeName);
        }

        #endregion

    }
}
