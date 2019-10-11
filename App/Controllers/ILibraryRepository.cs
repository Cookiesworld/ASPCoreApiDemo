using System.Collections.Generic;
using Authors.Models;

namespace Authors.Controllers
{
    public interface ILibraryRepository
    {
        IEnumerable<Writer> GetWriters();
    }

    public class LibraryRepositoryMock : ILibraryRepository
    {
        public IEnumerable<Writer> GetWriters()
        {
            return new List<Writer> {
                new Writer("Stephen King", null),
                new Writer("Neil Gaiman", null)
            };
        }
    }
}