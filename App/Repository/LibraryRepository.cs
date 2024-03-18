using System;
using System.Collections.Generic;
using Authors.Models;
using Dapper;

namespace Authors.Repository
{
    public class LibraryRepository(DataContext dataContext) : ILibraryRepository
    {
        public IEnumerable<Writer> GetWriters()
        {
            using var db = dataContext.CreateConnection();
            return db.Query<Writer>(@"SELECT [Id]
                                          ,[Name]
                                          ,[DateOfBirth]
                                          ,[GENDER]
                                      FROM [Writer]");
        }

        public Writer GetWriter(long id)
        {
            using var db = dataContext.CreateConnection();
            var writer = db.QueryFirstOrDefault<Writer>(@"SELECT [Id]
                                          ,[Name]
                                          ,[DateOfBirth]
                                          ,[GENDER] 
                                            FROM [Writer]
                                            where id =@Id", new { Id = id });
            return writer;
        }

        public int AddWriter(Writer writer)
        {
            if (writer == null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            using var db = dataContext.CreateConnection();


            var result = db.Execute("Insert Into Writer (Name, DateOfBirth, Gender) values (@Name, @DateOfBirth, @Gender)", new { writer.Name, writer.DateOfBirth, writer.Gender });
            return result;
        }
    }
}