using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Authors.Models;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Authors.Controllers
{
    public class LibraryRepositoryMock : ILibraryRepository
    {
        public IEnumerable<Writer> GetWriters()
        {
            return new List<Writer> {
                new Writer("Stephen King", null),
                new Writer("Neil Gaiman", null)
            };
        }

        public Writer GetWriter(long id)
        {
            if (id == 0)
            {
                return null;
            }

            return new Writer {
                Id = id
            };
        }
    }

    public class LibraryRepository : ILibraryRepository
    {
        private readonly IConfiguration config;

        public LibraryRepository(IConfiguration config)
        {
            this.config = config;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(this.config.GetConnectionString("MyConnectionString"));
            }
        }

        public IEnumerable<Writer> GetWriters()
        {
            using (IDbConnection db = Connection)
            {
                return db.Query<Writer>("Select * From Writer");
            }
        }

        public Writer GetWriter(long id)
        {
            using (IDbConnection db = Connection)
            {
                var writer = db.QueryFirstOrDefault<Writer>("Select * From Writer where id =@Id", new {Id = id});
                return writer;
            }
        }
    }
}