using System.Collections.Generic;
using Authors.Models;

namespace Authors.Repository
{
    public class LibraryRepositoryMock : ILibraryRepository
    {
        public IEnumerable<Writer> GetWriters() => new List<Writer> {
                new Writer("Stephen King", null),
                new Writer("Neil Gaiman", null)
            };
    }
}