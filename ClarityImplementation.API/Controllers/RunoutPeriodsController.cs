using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models.Plans;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RunoutPeriodsController : Controller
    {
        private readonly GenericRepository<RunoutPeriod> repository;

        public RunoutPeriodsController(ClarityDbContext context)
        {
            repository = new GenericRepository<RunoutPeriod>(context);
        }

        // GET: api/RunoutPeriods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RunoutPeriod>>> GetRunoutPeriods()
        {
            var runoutPeriods = await repository.GetAll();
            if (runoutPeriods == null)
            {
                return NotFound();
            }
            return Ok(runoutPeriods);
        }

        // GET: api/RunoutPeriods/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RunoutPeriod>> GetRunoutPeriod(int id)
        {
            var runoutPeriod = await repository.GetById(id);
            if (runoutPeriod == null)
            {
                return NotFound();
            }
            return Ok(runoutPeriod);
        }

        // PUT: api/RunoutPeriod/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRunoutPeriod(int id, RunoutPeriod runoutPeriod)
        {
            if (id != runoutPeriod.Id)
            {
                return BadRequest();
            }

            var updatedRunoutPeriod = await repository.Update(runoutPeriod, id);
            if (updatedRunoutPeriod == null)
            {
                return NotFound();
            }

            return Ok(updatedRunoutPeriod);
        }

        // POST: api/RunoutPeriods

        [HttpPost]
        public async Task<ActionResult<RunoutPeriod>> PostRunoutPeriod(RunoutPeriod runoutPeriod)
        {
            var addedRunoutPeriod = await repository.Add(runoutPeriod);
            if (addedRunoutPeriod == null)
            {
                return NotFound();
            }
            return Ok(addedRunoutPeriod);
        }

        // DELETE: api/RunoutPeriod/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRunoutPeriod(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }
    }
}
