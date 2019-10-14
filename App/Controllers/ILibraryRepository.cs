using System.Collections.Generic;
using Authors.Models;

namespace Authors.Controllers
{
    public interface ILibraryRepository
    {
        IEnumerable<Writer> GetWriters();
        Writer GetWriter(long id);
    }
}