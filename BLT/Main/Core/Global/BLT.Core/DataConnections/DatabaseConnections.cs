using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLT.Core.DataConnections
{
    public class DatabaseConnections
    {
        public DatabaseConnections()
        {
            _masterDatabaseName = "master";
            _platformDatabaseName = "Platform";
            _accountsDatabaseName = "Accounts";
            //_membershipTableName = ""; // <-- Set based on Account Partition
        }

        public string SqlServerName { get; set; }
        public string SqlUserName { get; set; }
        public string SqlPassword { get; set; }
        //============================================
        private string _platformDatabaseName { get; set; }
        private string _accountsDatabaseName { get; set; }
        //private string _membershipTableName { get; set; } // Remove as it is now sharded
        private string _masterDatabaseName { get; set; }

        public string SchemaUserName { get; set; }
        public string SchemaAccountName { get; set; }
        //==============================================


        public SqlConnection MasterSqlConnection
        {
            get
            {
                SqlConnection sqlConnection = new SqlConnection(_generateConnectionString(_masterDatabaseName));
                return sqlConnection;
            }
        }
        public SqlConnection PlatformSqlConnection
        {
            get
            {
                SqlConnection sqlConnection = new SqlConnection(_generateConnectionString(_platformDatabaseName));
                return sqlConnection;
            }
        }
        public SqlConnection AccountsSqlConnection
        {
            get
            {
                SqlConnection sqlConnection = new SqlConnection(_generateConnectionString(_accountsDatabaseName));
                return sqlConnection;
            }
        }

        /*
        public SqlConnection SetSqlConnection(string DatabasePartition)
        {
            SqlConnection sqlConnection = new SqlConnection(_generateConnectionString(DatabasePartition));
            return sqlConnection;

        }*/


        //public SqlConnection MembershipSqlConnection
        //{
        //    get
        //   {
        //      SqlConnection sqlConnection = new SqlConnection(_generateConnectionString(_membershipTableName));
        //       return sqlConnection;
        //  }
        //}

        private string _generateConnectionString(string databaseName)
        {
            return "Server=" + SqlServerName + ".database.windows.net;Database=" + databaseName + ";MultipleActiveResultSets=true;User ID=" + SqlUserName + ";Password=" + SqlPassword + ";Trusted_Connection=False;Encrypt=True;";
        }
    }
}
