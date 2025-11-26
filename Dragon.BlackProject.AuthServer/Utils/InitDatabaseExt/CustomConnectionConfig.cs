using SqlSugar;
namespace Dragon.BlackProject.AuthServer.Utils.InitDatabaseExt
{
    public class CustomConnectionConfig
    {
        public required string DefaultConnection { get; set; }
        public required string DbType { get; set; }
        public bool IsAutoCloseConnection { get; set; } = true;
        public bool EnableSqlLog { get; set; } = false;
    }
}
