using Microsoft.AspNetCore.Mvc;
using Persons.Data;
using Persons_api.Exceptions;

namespace Persons_api.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class PersonController : ControllerBase
    {
        private readonly InMemoryPersonRepository _repository;

        public PersonController(InMemoryPersonRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("hello")]
        public IActionResult HelloWorld()
        {
            var apiVersion = HttpContext.GetRequestedApiVersion().ToString();
            return Ok($"Hello World! Version {apiVersion}");
        }

        [HttpGet("persons")]
        public IActionResult GetAllPersons()
        {
            try
            {
                var persons = _repository.GetAllPersons();
                if (persons == null)
                {
                    throw new NotFoundException("No persons found.");
                }
                return Ok(persons);
            }
            catch (Exception ex)
            {
                throw new CustomControllerException("Error while fetching persons.", ex);
            }
        }

        [HttpGet("persons/{id}")]
        public IActionResult GetPersonById(int id)
        {
            try
            {
                var person = _repository.GetPersonById(id);
                if (person == null)
                {
                    throw new NotFoundException($"Person with ID {id} not found.");
                }
                return Ok(person);
            }
            catch (Exception ex)
            {
                throw new CustomControllerException($"Error while fetching person with ID {id}.", ex);
            }
        }
    }
}
