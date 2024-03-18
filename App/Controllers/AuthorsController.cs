using System.Linq;
using System.Net;
using Authors.Models;
using Authors.Repository;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.AspNetCore.Http;

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
        [HttpGet("/authors")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult GetAuthors()
        {
            var writers = libraryRepository.GetWriters();
            if (writers == null || !writers.Any())
            {
                return NotFound();
            }

            return Ok(writers);
        }

        /// <summary>
        /// Gets an author
        /// </summary>      
        /// <response code="200">Returns an authors</response>  
        /// <response code="404">No author found</response>  
        [HttpGet("/author/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAuthor(long id)
        {
            var writer = libraryRepository.GetWriter(id);
            if (writer == null)
            {
                return NotFound(id);
            }

            return Ok(writer);
        }

        [HttpPost("/Author")]
        [ProducesResponseType(201)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType(typeof(int))]
        public IActionResult Insert([FromBody] Writer author)
        {
            var response = libraryRepository.AddWriter(author);
            if (response == 1)
            {
                return Accepted(response);
            }

            return BadRequest();
        }
    }
}