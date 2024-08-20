using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrokerRolesController : Controller
    {
        private readonly GenericRepository<BrokerRole> repository;

        public BrokerRolesController(ClarityDbContext context)
        {
            repository = new GenericRepository<BrokerRole>(context);
        }

        // GET: api/BrokerRoles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrokerRole>>> GetBrokerRoles()
        {
            var brokerRoles = await repository.GetAll();
            if (brokerRoles == null)
            {
                return NotFound();
            }
            return Ok(brokerRoles);
        }

        // GET: api/BrokerRoles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BrokerRole>> GetBrokerRole(int id)
        {
            var brokerRole = await repository.GetById(id);
            if (brokerRole == null)
            {
                return NotFound();
            }
            return Ok(brokerRole);
        }

        // PUT: api/BrokerRoles/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrokerRole(int id, BrokerRole brokerRole)
        {
            if (id != brokerRole.Id)
            {
                return BadRequest();
            }

            var updatedBrokerRole = await repository.Update(brokerRole, id);
            if (updatedBrokerRole == null)
            {
                return NotFound();
            }
            return Ok(updatedBrokerRole);
        }

        // POST: api/BrokerRoles

        [HttpPost]
        public async Task<ActionResult<ContactRole>> PostBrokerRole(BrokerRole brokerRole)
        {
            var addedBrokerRole = await repository.Add(brokerRole);
            if (addedBrokerRole == null)
            {
                return NotFound();
            }
            return Ok(addedBrokerRole);
        }

        // DELETE: api/BrokerRoles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrokerRole(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }
    }
}
