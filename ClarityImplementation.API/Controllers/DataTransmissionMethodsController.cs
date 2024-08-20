using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models;
using ClarityImplementation.API.Models.Plans;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataTransmissionMethodsController : Controller
    {
        private readonly GenericRepository<DataTransmissionMethod> repository;

        public DataTransmissionMethodsController(ClarityDbContext context)
        {
            repository = new GenericRepository<DataTransmissionMethod>(context);
        }

        // GET: api/DataTransmissionMethods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DataTransmissionMethod>>> GetDataTransmissionMethods()
        {
            var dataTransmissionMethods = await repository.GetAll();
            if (dataTransmissionMethods == null)
            {
                return NotFound();
            }
            return Ok(dataTransmissionMethods);
        }

        // GET: api/DataTransmissionMethods/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DataTransmissionMethod>> GetDataTransmissionMethod(int id)
        {
            var dataTransmissionMethod = await repository.GetById(id);
            if (dataTransmissionMethod == null)
            {
                return NotFound();
            }
            return Ok(dataTransmissionMethod);
        }

        // PUT: api/DataTransmissionMethod/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDataTransmissionMethod(int id, DataTransmissionMethod dataTransmissionMethod)
        {
            if (id != dataTransmissionMethod.Id)
            {
                return BadRequest();
            }

            var updatedDataTransmissionMethod = await repository.Update(dataTransmissionMethod, id);
            if (updatedDataTransmissionMethod == null)
            {
                return NotFound();
            }

            return Ok(updatedDataTransmissionMethod);
        }

        // POST: api/DataTransmissionMethod

        [HttpPost]
        public async Task<ActionResult<DataTransmissionMethod>> PostDataTransmissionMethod(DataTransmissionMethod dataTransmissionMethod)
        {
            var addedDataTransmissionMethod = await repository.Add(dataTransmissionMethod);
            if (addedDataTransmissionMethod == null)
            {
                return NotFound();
            }
            return Ok(addedDataTransmissionMethod);
        }

        // DELETE: api/DataTransmissionMethods/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDataTransmissionMethod(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }
    }
}
