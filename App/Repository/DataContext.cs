using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace Authors.Repository
{
    public class DataContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IDbConnection CreateConnection()
        {
            return new SqliteConnection(Configuration.GetConnectionString("MyConnectionString"));
        }

        public async Task Init()
        {
            // create database tables if they don't exist
            using var connection = CreateConnection();
            await _initUsers();

            async Task _initUsers()
            {
                var sql = """
                CREATE TABLE IF NOT EXISTS 
                [dbo].Writer (
                    Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    Name TEXT,
                    DateOfBirth Date,
                    Gender TEXT
                );
            """;
                await connection.ExecuteAsync(sql);
            }
        }
    }
}