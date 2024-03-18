using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Authors.Repository
{
    public class DataContext
    {
        protected readonly IConfiguration Configuration;
        private readonly ILogger<DataContext> logger;

        public DataContext(IConfiguration configuration, ILogger<DataContext> logger)
        {
            Configuration = configuration;
            this.logger = logger;
        }

        public IDbConnection CreateConnection()
        {
            return new SqliteConnection(Configuration.GetConnectionString("MyConnectionString"));
        }

        public void Init()
        {
            var initCount = InitUsers();
            logger.LogDebug($"Init {initCount} users");
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