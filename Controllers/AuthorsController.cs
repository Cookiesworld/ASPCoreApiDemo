using System.Linq;
using Authors.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Authors.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class AuthorsController : Controller
    {
        private readonly ILibraryRepository libraryRepository;

        /// <summary>
        /// Authors repository
        /// </summary>
        /// <param name="libraryRepository"></param>
        public AuthorsController(ILibraryRepository libraryRepository)
        {
            this.libraryRepository = libraryRepository ?? throw new System.ArgumentNullException(nameof(libraryRepository));
        }


        /// <summary>
        /// Gets a colleciton of authors
        /// </summary>      
        /// <response code="200">Returns list of authors</response>  
        /// <response code="404">No authors found</response>  
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult Get()
        {
            var writers = this.libraryRepository.GetWriters();
            if (!writers.Any())
            {
                return NotFound();
            }

            return Ok(writers);
        }
    }
}