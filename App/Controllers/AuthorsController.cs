using System;
using System.Collections.Generic;
using System.Linq;
using Authors.Models;
using Authors.Repository;
using Microsoft.AspNetCore.Mvc;
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
        [ProducesDefaultResponseType(typeof(List<Writer>))]
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
        [ProducesDefaultResponseType(typeof(Writer))]
        public IActionResult GetAuthor(long id)
        {
            var writer = libraryRepository.GetWriter(id);
            if (writer == null)
            {
                return NotFound(id);
            }

            return Ok(writer);
        }

        /// <summary>
        /// Insert an author
        /// </summary>
        /// <param name="author">the author to insert</param>
        /// <returns>Count of insertions</returns>
        [HttpPost("/author")]
        [ProducesResponseType(201)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType(typeof(int))]
        public IActionResult Insert([FromBody] Writer author)
        {
            ArgumentNullException.ThrowIfNull(author);

            var response = libraryRepository.AddWriter(author);
            if (response == 1)
            {
                return Accepted(response);
            }

            return BadRequest();
        }
    }
}