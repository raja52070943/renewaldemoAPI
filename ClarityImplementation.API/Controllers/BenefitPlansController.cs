using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models.Plans;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BenefitPlansController : Controller
    {
        private readonly GenericRepository<BenefitPlan> repository;

        public BenefitPlansController(ClarityDbContext context)
        {
            repository = new GenericRepository<BenefitPlan>(context);
        }

        // GET: api/BenefitPlans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BenefitPlan>>> GetBenefitPlans()
        {
            var benefitPlans = await repository.GetAll();
            if (benefitPlans == null)
            {
                return NotFound();
            }
            return Ok(benefitPlans);
        }

        // GET: api/BenefitPlan/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BenefitPlan>> GetBenefitPlan(int id)
        {
            var benefitPlan = await repository.GetById(id);
            if (benefitPlan == null)
            {
                return NotFound();
            }
            return Ok(benefitPlan);
        }

        // PUT: api/BenefitPlan/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBenefitPlan(int id, BenefitPlan benefitPlan)
        {
            if (id != benefitPlan.Id)
            {
                return BadRequest();
            }

            var updatedBenefitPlan = await repository.Update(benefitPlan, id);
            if (updatedBenefitPlan == null)
            {
                return NotFound();
            }

            return Ok(updatedBenefitPlan);
        }

        // POST: api/BenefitPlan

        [HttpPost]
        public async Task<ActionResult<BenefitPlan>> PostBenefitPlan(BenefitPlan benefitPlan)
        {
            var addedBenefitPlan = await repository.Add(benefitPlan);
            if (addedBenefitPlan == null)
            {
                return NotFound();
            }
            return Ok(addedBenefitPlan);
        }

        // DELETE: api/BenefitPlan/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBenefitPlan(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }
    }
}
