using System.Collections.Generic;
using Authors.Models;
using Authors.Repository;

namespace Authors.Controllers
{
    public class LibraryRepositoryMock : ILibraryRepository
    {
        public IEnumerable<Writer> GetWriters()
        {
            return [
                new("Stephen King", null, Gender.Male),
                new("Neil Gaiman", null, Gender.Male)
            ];
        }

        public Writer GetWriter(long id)
        {
            if (id == 0)
            {
                return null;
            }

            return new Writer
            {
                Id = id
            };
        }

        public int AddWriter(Writer wrtier)
        {
            return 1;
        }
    }
}