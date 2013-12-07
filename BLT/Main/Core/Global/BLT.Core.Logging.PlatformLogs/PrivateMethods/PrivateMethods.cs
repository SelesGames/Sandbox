using BLT.Core.Logging.PlatformLogs.TableEntities;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLT.Core.Logging.PlatformLogs
{
    internal static class PrivateMethods
    {

        #region Write to Logs

        internal static void WritePlatformLog(IPlatformLogType activity, string description, string details = null)
        {
            CloudTableClient cloudTableClient = EnvironmentSettings.StorageConnection.PlatformStorage.CreateCloudTableClient();

            //Create an instance of each entity type and pass in associated CloudTableClient & TableName
            PlatformLogTableEntity_ByActivity logTableEntity_Activity = new PlatformLogTableEntity_ByActivity(cloudTableClient, activity.LogName.ToLower() + "" + "byactivity");
            PlatformLogTableEntity_ByTime logTableEntity_Time = new PlatformLogTableEntity_ByTime(cloudTableClient, activity.LogName.ToLower() + "" + "bytime");

            //Gather up all the entities into a list for our parallel task to execute in a ForEach
            List<Object> entityTypes = new List<object>();
            entityTypes.Add(logTableEntity_Activity);
            entityTypes.Add(logTableEntity_Time);

            try
            {

                Parallel.ForEach(entityTypes, obj =>
                {

                    #region Trace Statements

                    //Display the id of the thread for each parallel instance to verifiy prallelism
                    //Trace.TraceInformation("Current thread ID: " + Thread.CurrentThread.ManagedThreadId);

                    #endregion

                    //Transform the LogItem into each corresponding table entity type for insert execution into logs
                    (obj as IPlatformLogTableEntity).ActivityType = activity.Activity;
                    (obj as IPlatformLogTableEntity).Description = description;
                    (obj as IPlatformLogTableEntity).Details = details;

                    //Create table for entity if not exists
                    (obj as IPlatformLogTableEntity).cloudTable.CreateIfNotExists();

                    //create an insert operation for each entity, assign to designated CloudTable, and add to our list of tasks:
                    TableOperation operation = TableOperation.Insert((obj as TableEntity));
                    (obj as IPlatformLogTableEntity).cloudTable.Execute(operation);
                });
            }
            catch (Exception e)
            {

            }

        }

        #endregion


        #region Get Full Logs By Time

        /// <summary>
        /// Get all logs from a log type by time
        /// </summary>
        /// <param name="platformLogName"></param>
        /// <param name="maxRecords"></param>
        /// <returns></returns>
        internal static IEnumerable<PlatformLogTableEntity> GetFullLogByTime(PlatformLogName platformLogName, int maxRecords)
        {
            string logName = platformLogName.ToString().ToLower() + "bytime";

            CloudTableClient cloudTableClient = EnvironmentSettings.StorageConnection.PlatformStorage.CreateCloudTableClient();
            CloudTable cloudTable = cloudTableClient.GetTableReference(logName);

            cloudTable.CreateIfNotExists();

            TableQuery<PlatformLogTableEntity> query = new TableQuery<PlatformLogTableEntity>().Take(maxRecords);

            return cloudTable.ExecuteQuery(query);
        }

        #endregion

        #region Get Activity Specific Records from a LogType

        /// <summary>
        /// Get activity specific records from a log type
        /// </summary>
        /// <param name="activity"></param>
        /// <param name="maxRecords"></param>
        /// <returns></returns>
        internal static IEnumerable<PlatformLogTableEntity> GetActivityLogByTime(PlatformLogType activity, int maxRecords)
        {
            string logName = activity.LogName.ToLower() + "byactivity";

            CloudTableClient cloudTableClient = EnvironmentSettings.StorageConnection.PlatformStorage.CreateCloudTableClient();
            CloudTable cloudTable = cloudTableClient.GetTableReference(logName);

            cloudTable.CreateIfNotExists();

            TableQuery<PlatformLogTableEntity> query = new TableQuery<PlatformLogTableEntity>().Take(maxRecords).
                Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, activity.Activity));

            return cloudTable.ExecuteQuery(query);
        }


        #endregion


        #region Clear Logs

        /// <summary>
        /// Clear all log tables/data
        /// </summary>
        public static bool ClearLogs()
        {

            CloudTableClient cloudTableClient = EnvironmentSettings.StorageConnection.PlatformStorage.CreateCloudTableClient();


            foreach (string logName in Enum.GetNames(typeof(PlatformLogName)))
            {
                foreach (string partitionKeyType in Enum.GetNames(typeof(PlatformLogPartition)))
                {
                    CloudTable cloudTable = cloudTableClient.GetTableReference(logName.ToLower() + "by" + partitionKeyType.ToLower());
                    cloudTable.DeleteIfExists();
                }
            }

            return true;
        }

        /// <summary>
        /// Clear log tables/data for a particular LogType
        /// </summary>
        public static bool ClearLog(PlatformLogName platformLogTypeName)
        {

            CloudTableClient cloudTableClient = EnvironmentSettings.StorageConnection.PlatformStorage.CreateCloudTableClient();

            foreach (string partitionKeyType in Enum.GetNames(typeof(PlatformLogPartition)))
            {
                CloudTable cloudTable = cloudTableClient.GetTableReference(platformLogTypeName.ToString().ToLower() + "by" + partitionKeyType.ToLower());
                cloudTable.DeleteIfExists();
            }

            return true;
        }

        #endregion

    }
}
