using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http.Description;
using Authors.Models;
using Authors.Repository;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.Swagger.Annotations;

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
            var writers = this.libraryRepository.GetWriters();
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
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [SwaggerResponse(HttpStatusCode.OK, Type=typeof(Writer))]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public IActionResult GetAuthor(long id)
        {
            var writer = this.libraryRepository.GetWriter(id);
            if (writer == null)
            {
                return NotFound(id);
            }

            return Ok(writer);
        }

        [HttpPost("/Author")]
        [ResponseType(typeof(AcceptedResult))]
        [SwaggerResponse(HttpStatusCode.Accepted)]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        public IActionResult Insert([FromBody] Writer author)
        {
            if (this.libraryRepository.AddWriter(author) == 1)
            {
                return this.Accepted();
            }

            return BadRequest();
        }
    }
}