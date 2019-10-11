using Microsoft.AspNetCore.Mvc;

namespace Authors.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorsController : Controller
    {
        private readonly ILibraryRepository libraryRepository;

        public AuthorsController(ILibraryRepository libraryRepository)
        {
            this.libraryRepository = libraryRepository ?? throw new System.ArgumentNullException(nameof(libraryRepository));
        }

        [HttpGet]
        public IActionResult Get()
        {
            var writers = this.libraryRepository.GetWriters();
            return Ok(writers);
        }
    }
}