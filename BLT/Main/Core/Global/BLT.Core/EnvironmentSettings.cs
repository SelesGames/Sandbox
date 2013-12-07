using BLT.Core.DataConnections;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLT.Core
{
    public static class EnvironmentSettings
    {

        #region Private Properties

        private static DatabaseConnections _sqlDataConnections;
        private static StorageConnections _storageConnections;

        #endregion

        #region Initialization

        /// <summary>
        /// Called during startup by each shared project
        /// </summary>
        public static void Initialize()
        {
            #region Initialize Database Connections

            _sqlDataConnections = new DatabaseConnections();

            switch (Environment.Current.ToLower())
            {

                #region Production

                case "production":
                    _sqlDataConnections.SqlServerName = "oqj6b9n8qh";
                    _sqlDataConnections.SqlUserName = "BLTClientExtranetDB_Production";
                    _sqlDataConnections.SqlPassword = "!BaconTomatoLettuceWrapsX3*";
                    break;

                #endregion


                #region Stage

                case "stage":
                    _sqlDataConnections.SqlServerName = "p9hs0x7ppr";
                    _sqlDataConnections.SqlUserName = "BLTClientExtranetDB_Stage";
                    _sqlDataConnections.SqlPassword = "BaconLettuceWraps456";
                    break;

                #endregion


                #region Local/Debug

                case "debug":
                    _sqlDataConnections.SqlServerName = "ze64uj4f8z";
                    _sqlDataConnections.SqlUserName = "BLTClientExtranetDB_Debug";
                    _sqlDataConnections.SqlPassword = "BaconLettuceWraps123";
                    break;

                case "local":
                    _sqlDataConnections.SqlServerName = "ze64uj4f8z";
                    _sqlDataConnections.SqlUserName = "BLTClientExtranetDB_Debug";
                    _sqlDataConnections.SqlPassword = "BaconLettuceWraps123";
                    break;

                #endregion

            }

            #endregion

            #region Initialize Storage Connections

            switch (Environment.Current.ToLower())
            {


                #region Production

                case "production":
                    _storageConnections = new StorageConnections(

                        //Platform (Production)
                        "bltplatformproduction",
                        "6CANwClOo0nou1xiAhgMUlqx9hEwc+pWHpSiOvdoXD6pGvuxjintk0f3+YWJIQtoFqOKKfLz1bwiehDz/pe2kg==",

                        //Accounts (Production)
                        "bltaccountsproduction",
                        "p9fBtohGvjUrjGYF6nX4eVNLFfVgE6AF9tgnYfg3w+7fgW7JU4ws0UiHUsrNkcK7W6oDxOKbRGOnP8jEVvC8fQ=="
                        );
                    break;

                #endregion


                #region Stage

                case "stage":
                    _storageConnections = new StorageConnections(

                        //Platform (Stage)
                        "bltplatformstage",
                        "834TeorlK8OZfmlRS7Fe/G/zwL1AYHV/O2ntLGPTurqMc/jVFWE6ECdYXOQQo1FNU73y+G71+Kyy4MG4WZV1TA==",

                        //Accounts (Stage)
                        "bltaccountsstage",
                        "VJEws8Ysqr7Tz7wosprTX8oFxigOZFIWGR6obpgJ/KNQZxJfdOSpDrcAodeNAD4tIxh2ziGXUBurBcg3eZmmpw=="
                        );
                    break;


                #endregion

                #region Local/Debug

                case "debug":
                    _storageConnections = new StorageConnections(

                        //Platform (Debug)
                        "bltplatformdebug",
                        "tVEEEyg74acuJgL6GjwSq+XNeMAQZRuUbqf7wTqQ1FILykcqii8CERqLqaGULASsHAfA7k4BcU+9t6OnbL2rqA==",

                        //Accounts (Debug)
                        "bltaccountsdebug",
                        "2XKM26TvfOqcQiK60J2IG0Zf0lJjvEOoR2ppfr+5Y8+OJ81x1Y+Xi0if7+o5rfWHlkEYVn5e1UwYJUj2DKxrwQ=="
                        );
                    break;


                case "local":
                    _storageConnections = new StorageConnections(

                        //Platform (Local)
                        "bltplatformdebug",
                        "tVEEEyg74acuJgL6GjwSq+XNeMAQZRuUbqf7wTqQ1FILykcqii8CERqLqaGULASsHAfA7k4BcU+9t6OnbL2rqA==",

                        //Accounts (Local)
                        "bltaccountsdebug",
                        "2XKM26TvfOqcQiK60J2IG0Zf0lJjvEOoR2ppfr+5Y8+OJ81x1Y+Xi0if7+o5rfWHlkEYVn5e1UwYJUj2DKxrwQ=="
                        );
                    break;

                #endregion

                default:
                    _storageConnections = null;
                    break;


            }

            #endregion
        }

        #endregion

        /// <summary>
        /// This is the one setting that all parent projects MUST have in their .Config files
        /// </summary>
        public static class Environment
        {
            public static string Current = ConfigurationManager.AppSettings["Environment"];
        }

        public static DatabaseConnections DatabaseConnections
        {
            get
            {

                return _sqlDataConnections;
            }

        }

        public static StorageConnections StorageConnection
        {
            get
            {
                return _storageConnections;
            }
        }


        public static class SendGridAccount
        {
            public static string UserName = ""; 
            public static string APIKey = ""; 
        }

    }
}
