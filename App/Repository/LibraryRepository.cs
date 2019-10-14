using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Authors.Models;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Authors.Repository
{
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
            using (var db = Connection)
            {
                return db.Query<Writer>("Select * From Writer");
            }
        }

        public Writer GetWriter(long id)
        {
            using (var db = Connection)
            {
                var writer = db.QueryFirstOrDefault<Writer>("Select * From Writer where id =@Id", new { Id = id });
                return writer;
            }
        }

        public int AddWriter(Writer writer)
        {
            if (writer == null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            using (var db = Connection)
            {
                var result = db.Execute("Insert Into Writer (Name, DateOfBirth) values (@name, @dateOfBirth)", new { Name = writer.Name, DateOfBirth = writer.DateOfBirth });
                return result;
            }
        } 
    }
}