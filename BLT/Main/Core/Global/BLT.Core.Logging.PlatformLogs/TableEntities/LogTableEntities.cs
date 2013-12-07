using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLT.Core.Logging.PlatformLogs.TableEntities
{
    #region Interface

    /// <summary>
    /// Interface that allows for greater brevity in our methods, specifically the Parallel.For loop for writing to Azure Storage.
    /// </summary>
    internal interface IPlatformLogTableEntity
    {
        string ActivityType { get; set; }
        string Description { get; set; }
        string Details { get; set; }


        CloudTable cloudTable { get; set; }

    }

    #endregion

    #region Base Class

    /// <summary>
    /// Base class for all LogTableEntity Types
    /// </summary>
    abstract class PlatformLogTableEntity_Base : TableEntity, IPlatformLogTableEntity
    {
        public PlatformLogTableEntity_Base(CloudTableClient cloudTableClient, string tableName)
        {
            //Create the cloudtable instance and  name for the entity operate against:
            cloudTable = cloudTableClient.GetTableReference(tableName);
            cloudTable.CreateIfNotExists();
        }

        // Abstract properties (properties that are used for partition keys on LogTableEntity_ types)
        public abstract string ActivityType { get; set; }


        // Base Properties

        public string Description { get; set; }
        public string Details { get; set; }


        public CloudTable cloudTable { get; set; }
    }

    #endregion

    #region Table Entities



    internal class PlatformLogTableEntity_ByActivity : PlatformLogTableEntity_Base
    {
        public PlatformLogTableEntity_ByActivity(CloudTableClient cloudTableClient, string tableName)
            : base(cloudTableClient, tableName)
        {
            RowKey = string.Format("{0:d19}+{1}", DateTime.MaxValue.Ticks - DateTime.UtcNow.Ticks, Guid.NewGuid().ToString("N"));
        }

        public override string ActivityType
        {
            get { return PartitionKey; }
            set { PartitionKey = value; }
        }


    }

    internal class PlatformLogTableEntity_ByTime : PlatformLogTableEntity_Base
    {
        public PlatformLogTableEntity_ByTime(CloudTableClient cloudTableClient, string tableName)
            : base(cloudTableClient, tableName)
        {
            PartitionKey = string.Format("{0:d19}+{1}", DateTime.MaxValue.Ticks - DateTime.UtcNow.Ticks, Guid.NewGuid().ToString("N"));
        }

        public override string ActivityType
        {
            get { return RowKey; }
            set { RowKey = value; }
        }


    }

    #endregion
}
