using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models.Communication;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessEmployeeCommunicationsController : Controller
    {
        private readonly GenericRepository<AccessEmployeeCommunication> repository;
        public AccessEmployeeCommunicationsController(ClarityDbContext context)
        {
            repository = new GenericRepository<AccessEmployeeCommunication>(context);
        }

        // GET: api/AccessEmployeeCommunications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccessEmployeeCommunication>>> GetAccessEmployeeCommunications()
        {
            var accessEmployeeCommunications = await repository.GetAll();
            if (accessEmployeeCommunications == null)
            {
                return NotFound();
            }
            return Ok(accessEmployeeCommunications);
        }

        // GETById: api/AccessEmployeeCommunications/id
        [HttpGet("{id}")]
        public async Task<ActionResult<AccessEmployeeCommunication>> GetAccessEmployeeCommunication(int id)
        {
            var accessEmployeeCommunication = await repository.GetById(id);
            if (accessEmployeeCommunication == null)
            {
                return NotFound();
            }
            return Ok(accessEmployeeCommunication);
        }
        // PUT: api/AccessEmployeeCommunications/id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccessEmployeeCommunication(int id, AccessEmployeeCommunication accessEmployeeCommunication)
        {
            if (id != accessEmployeeCommunication.Id)
            {
                return BadRequest();
            }

            var updatedAccessEmployeeCommunication = await repository.Update(accessEmployeeCommunication, id);
            if (updatedAccessEmployeeCommunication == null)
            {
                return NotFound();
            }
            return Ok(updatedAccessEmployeeCommunication);
        }
        // POST: api/AccessEmployeeCommunications
        [HttpPost]
        public async Task<ActionResult<AccessEmployeeCommunication>> PostAccessEmployeeCommunication(AccessEmployeeCommunication accessEmployeeCommunication)
        {

            var addedAccessEmployeeCommunication = await repository.Add(accessEmployeeCommunication);
            if (addedAccessEmployeeCommunication == null)
            {
                return NotFound();
            }
            return Ok(addedAccessEmployeeCommunication);
        }
        // DELETE: api/AccessEmployeeCommunications/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccessEmployeeCommunication(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }
    }
}
