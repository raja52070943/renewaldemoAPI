using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models.Plans;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EBPlansController : Controller
    {
        private readonly GenericRepository<EBPlan> repository;

        public EBPlansController(ClarityDbContext context)
        {
            repository = new GenericRepository<EBPlan>(context);
        }

        // GET: api/Addresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EBPlan>>> GetEBPlans()
        {
            var ebPlans = await repository.GetAll();
            if (ebPlans == null)
            {
                return NotFound();
            }
            return Ok(ebPlans);
        }

        // GET: api/Addresses/5
        [HttpGet("{id}")]
        //[HttpGet("{id}")]
        public async Task<ActionResult<EBPlan>> GetEBPlan(int id)
        {
            var ebPlan = await repository.GetById(id);
            if (ebPlan == null)
            {
                return NotFound();
            }
            return Ok(ebPlan);
        }

        // PUT: api/Addresses/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEBPlan(int id, EBPlan ebPlan)
        {
            if (id != ebPlan.Id)
            {
                return BadRequest();
            }

            var updatedEBPlan = await repository.Update(ebPlan, id);
            if (updatedEBPlan == null)
            {
                return NotFound();
            }
            return Ok(updatedEBPlan);
        }

        // POST: api/Addresses

        [HttpPost]
        public async Task<ActionResult<EBPlan>> PostEBPlan(EBPlan ebPlan)
        {
            var addedEBPlan = await repository.Add(ebPlan);
            if (addedEBPlan == null)
            {
                return NotFound();
            }
            return Ok(addedEBPlan);
        }


        // DELETE: api/Addresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEBPlan(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }
    }
}
