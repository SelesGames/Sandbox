﻿
namespace Sandbox.Data.Entity
{
    public static class Connection
    {
        static string dataSource = @"(LocalDB)\v11.0";
        private static string _filePath = @"C:\WORK\CODE\SELES GAMES\Sandbox\BLT.Sandbox\Sandbox\Sandbox.WebApp\App_Data\test_db.mdf";
        // todo: relative or app/web.config
        static string FilePath { get {
            switch (System.Environment.MachineName)
            {
                case "WORKSTATION-1":
                    _filePath = @"C:\Users\Eric\Documents\Visual Studio 2013\Projects\SelesGames\Sandbox\BLT.Sandbox\Sandbox\Sandbox.WebApp\App_Data\test_db.mdf";
                    break;
            }
            return _filePath;
        } }

        public static string CONNECTION_STRING = string.Format(
"Data Source={0};AttachDbFilename=\"{1}\";Integrated Security=True;Connect Timeout=30",
dataSource,
FilePath);
    }
}