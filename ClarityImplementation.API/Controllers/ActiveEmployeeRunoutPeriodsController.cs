using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models.Plans;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActiveEmployeeRunoutPeriodsController : Controller
    {
        private readonly GenericRepository<ActiveEmployeeRunoutPeriod> repository;

        public ActiveEmployeeRunoutPeriodsController(ClarityDbContext context)
        {
            repository = new GenericRepository<ActiveEmployeeRunoutPeriod>(context);
        }

        // GET: api/InformationProviders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActiveEmployeeRunoutPeriod>>> GetActiveEmployeeRunoutPeriods()
        {
            var activeEmployeeRunoutPeriods = await repository.GetAll();
            if (activeEmployeeRunoutPeriods == null)
            {
                return NotFound();
            }
            return Ok(activeEmployeeRunoutPeriods);
        }

        // GET: api/InformationProviders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ActiveEmployeeRunoutPeriod>> GetActiveEmployeeRunoutPeriod(int id)
        {
            var activeEmployeeRunoutPeriod = await repository.GetById(id);
            if (activeEmployeeRunoutPeriod == null)
            {
                return NotFound();
            }
            return Ok(activeEmployeeRunoutPeriod);
        }

        // PUT: api/InformationProvider/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutActiveEmployeeRunoutPeriod(int id, ActiveEmployeeRunoutPeriod activeEmployeeRunoutPeriod)
        {
            if (id != activeEmployeeRunoutPeriod.Id)
            {
                return BadRequest();
            }

            var updatedActiveEmployeeRunoutPeriod = await repository.Update(activeEmployeeRunoutPeriod, id);
            if (updatedActiveEmployeeRunoutPeriod == null)
            {
                return NotFound();
            }

            return Ok(updatedActiveEmployeeRunoutPeriod);
        }

        // POST: api/InformationProviders

        [HttpPost]
        public async Task<ActionResult<ActiveEmployeeRunoutPeriod>> PostActiveEmployeeRunoutPeriod(ActiveEmployeeRunoutPeriod activeEmployeeRunoutPeriod)
        {
            var addedActiveEmployeeRunoutPeriod = await repository.Add(activeEmployeeRunoutPeriod);
            if (addedActiveEmployeeRunoutPeriod == null)
            {
                return NotFound();
            }
            return Ok(addedActiveEmployeeRunoutPeriod);
        }

        // DELETE: api/InformationProviders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActiveEmployeeRunoutPeriod(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }
    }
}
