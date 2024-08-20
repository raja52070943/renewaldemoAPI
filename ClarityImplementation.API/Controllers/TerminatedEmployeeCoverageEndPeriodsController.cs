using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models.Plans;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TerminatedEmployeeCoverageEndPeriodsController : Controller
    {
        private readonly GenericRepository<TerminatedEmployeeCoverageEndPeriod> repository;

        public TerminatedEmployeeCoverageEndPeriodsController(ClarityDbContext context)
        {
            repository = new GenericRepository<TerminatedEmployeeCoverageEndPeriod>(context);
        }

        // GET: api/TerminatedEmployeeCoverageEndPeriods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TerminatedEmployeeCoverageEndPeriod>>> GetTerminatedEmployeeCoverageEndPeriods()
        {
            var terminatedEmployeeCoverageEndPeriods = await repository.GetAll();
            if (terminatedEmployeeCoverageEndPeriods == null)
            {
                return NotFound();
            }
            return Ok(terminatedEmployeeCoverageEndPeriods);
        }

        // GET: api/TerminatedEmployeeCoverageEndPeriods/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TerminatedEmployeeCoverageEndPeriod>> GetTerminatedEmployeeCoverageEndPeriod(int id)
        {
            var terminatedEmployeeCoverageEndPeriod = await repository.GetById(id);
            if (terminatedEmployeeCoverageEndPeriod == null)
            {
                return NotFound();
            }
            return Ok(terminatedEmployeeCoverageEndPeriod);
        }

        // PUT: api/TerminatedEmployeeCoverageEndPeriod/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTerminatedEmployeeCoverageEndPeriod(int id, TerminatedEmployeeCoverageEndPeriod terminatedEmployeeCoverageEndPeriod)
        {
            if (id != terminatedEmployeeCoverageEndPeriod.Id)
            {
                return BadRequest();
            }

            var updatedTerminatedEmployeeCoverageEndPeriod = await repository.Update(terminatedEmployeeCoverageEndPeriod, id);
            if (updatedTerminatedEmployeeCoverageEndPeriod == null)
            {
                return NotFound();
            }

            return Ok(updatedTerminatedEmployeeCoverageEndPeriod);
        }

        // POST: api/TerminatedEmployeeCoverageEndPeriods

        [HttpPost]
        public async Task<ActionResult<TerminatedEmployeeCoverageEndPeriod>> PostTerminatedEmployeeCoverageEndPeriod(TerminatedEmployeeCoverageEndPeriod terminatedEmployeeCoverageEndPeriod)
        {
            var addedTerminatedEmployeeCoverageEndPeriod = await repository.Add(terminatedEmployeeCoverageEndPeriod);
            if (addedTerminatedEmployeeCoverageEndPeriod == null)
            {
                return NotFound();
            }
            return Ok(addedTerminatedEmployeeCoverageEndPeriod);
        }

        // DELETE: api/TerminatedEmployeeCoverageEndPeriods/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTerminatedEmployeeCoverageEndPeriod(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }
    }
}
