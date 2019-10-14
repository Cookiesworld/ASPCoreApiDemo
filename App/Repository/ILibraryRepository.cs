using System.Collections.Generic;
using Authors.Models;

namespace Authors.Repository
{
    public interface ILibraryRepository
    {
        IEnumerable<Writer> GetWriters();
        Writer GetWriter(long id);
        int AddWriter(Writer author);
    }
}