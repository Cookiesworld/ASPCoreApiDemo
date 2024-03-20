using System.Data;
using System.IO;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Authors.Repository
{
    public class DataContext(IConfiguration configuration, ILogger<DataContext> logger)
    {
        protected readonly IConfiguration Configuration = configuration;
        protected readonly ILogger<DataContext> Logger = logger;

        public IDbConnection CreateConnection()
        {
            return new SqliteConnection(Configuration.GetConnectionString("AuthorsConnectionString"));
        }

        public void Init()
        {
            var initCount = InitUsers();
            Logger.LogDebug("Init {0} users", initCount);
        }

        /// <summary>
        /// Create database tables if they don't exist
        /// </summary>
        /// <returns></returns>
        private int InitUsers()
        {
            using var connection = CreateConnection();
            var sql = """
                CREATE TABLE IF NOT EXISTS 
                Writer (
                    Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    DateOfBirth Date NULL,
                    Gender INT NOT NULL
                );
            """;
            return connection.Execute(sql);
        }
    }
}