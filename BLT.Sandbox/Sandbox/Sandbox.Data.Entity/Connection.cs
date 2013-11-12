
namespace Sandbox.Data.Entity
{
    public static class Connection
    {
        static string dataSource = @"(LocalDB)\v11.0";
        static string filePath = @"C:\WORK\CODE\SELES GAMES\Sandbox\BLT.Sandbox\Sandbox\Sandbox.WebApp\App_Data\test_db.mdf";
        
        public static string CONNECTION_STRING = string.Format(
"Data Source={0};AttachDbFilename=\"{1}\";Integrated Security=True;Connect Timeout=30",
dataSource,
filePath);
    }
}