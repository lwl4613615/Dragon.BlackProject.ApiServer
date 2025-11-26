using SqlSugar;

namespace Dragon.BlackProject.AuthServer.Utils.InitDatabaseExt
{
    public static class InitSqlSugarExt
    {
        public static void InitSqlSugar(this WebApplicationBuilder builder)
        {
          var dbconfig= builder.Configuration.GetSection("ConnectionStrings").Get<CustomConnectionConfig>();
          builder.Services.AddSingleton<ISqlSugarClient>(s =>
          {
              var config = new ConnectionConfig()
              {
                  ConnectionString = dbconfig?.DefaultConnection,
                  DbType = (DbType)Enum.Parse(typeof(DbType), dbconfig.DbType),
                  IsAutoCloseConnection = dbconfig.IsAutoCloseConnection,                  
              };
              var sqlSugar = new SqlSugarScope(config, db =>
              {
                  if (dbconfig.EnableSqlLog)
                  {
                      db.Aop.OnLogExecuting = (sql, pars) =>
                      {
                          Console.WriteLine($"SQL: {sql}");
                          Console.WriteLine($"Params: {string.Join(", ", pars.Select(p => $"{p.ParameterName}={p.Value}"))}");
                          Console.WriteLine("---------------------------------");
                      };
                  }
               
              });

              return sqlSugar;

          });
        }
    }
}
