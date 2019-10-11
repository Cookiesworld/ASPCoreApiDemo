using Microsoft.AspNetCore.Mvc;

namespace Authors.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly ILibraryRepository libraryRepository;

        public AuthorsController(ILibraryRepository libraryRepository)
        {
            this.libraryRepository = libraryRepository;
        }

        public IActionResult GetAuthors()
        {
            return Ok(this.libraryRepository.GetWriters());
        }
    }
}