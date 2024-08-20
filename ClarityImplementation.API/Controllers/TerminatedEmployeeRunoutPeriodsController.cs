using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models.Plans;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TerminatedEmployeeRunoutPeriodsController : Controller
    {
        private readonly GenericRepository<TerminatedEmployeeRunoutPeriod> repository;

        public TerminatedEmployeeRunoutPeriodsController(ClarityDbContext context)
        {
            repository = new GenericRepository<TerminatedEmployeeRunoutPeriod>(context);
        }

        // GET: api/InformationProviders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TerminatedEmployeeRunoutPeriod>>> GetTerminatedEmployeeRunoutPeriods()
        {
            var terminatedEmployeeRunoutPeriods = await repository.GetAll();
            if (terminatedEmployeeRunoutPeriods == null)
            {
                return NotFound();
            }
            return Ok(terminatedEmployeeRunoutPeriods);
        }

        // GET: api/InformationProviders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TerminatedEmployeeRunoutPeriod>> GetTerminatedEmployeeRunoutPeriod(int id)
        {
            var terminatedEmployeeRunoutPeriod = await repository.GetById(id);
            if (terminatedEmployeeRunoutPeriod == null)
            {
                return NotFound();
            }
            return Ok(terminatedEmployeeRunoutPeriod);
        }

        // PUT: api/InformationProvider/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTerminatedEmployeeRunoutPeriod(int id, TerminatedEmployeeRunoutPeriod terminatedEmployeeRunoutPeriod)
        {
            if (id != terminatedEmployeeRunoutPeriod.Id)
            {
                return BadRequest();
            }

            var updatedTerminatedEmployeeRunoutPeriod = await repository.Update(terminatedEmployeeRunoutPeriod, id);
            if (updatedTerminatedEmployeeRunoutPeriod == null)
            {
                return NotFound();
            }

            return Ok(updatedTerminatedEmployeeRunoutPeriod);
        }

        // POST: api/InformationProviders

        [HttpPost]
        public async Task<ActionResult<TerminatedEmployeeRunoutPeriod>> PostTerminatedEmployeeRunoutPeriod(TerminatedEmployeeRunoutPeriod terminatedEmployeeRunoutPeriod)
        {
            var addedTerminatedEmployeeRunoutPeriod = await repository.Add(terminatedEmployeeRunoutPeriod);
            if (addedTerminatedEmployeeRunoutPeriod == null)
            {
                return NotFound();
            }
            return Ok(addedTerminatedEmployeeRunoutPeriod);
        }

        // DELETE: api/InformationProviders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTerminatedEmployeeRunoutPeriod(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }
    }
}
