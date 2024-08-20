using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models.Plans;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BenefitPlanTypesController : Controller
    {
        private readonly GenericRepository<BenefitPlanType> repository;

        public BenefitPlanTypesController(ClarityDbContext context)
        {
            repository = new GenericRepository<BenefitPlanType>(context);
        }

        // GET: api/BenefitPlanTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BenefitPlanType>>> GetBenefitPlanTypes()
        {
            var benefitPlanTypes = await repository.GetAll();
            if (benefitPlanTypes == null)
            {
                return NotFound();
            }
            return Ok(benefitPlanTypes);
        }

        // GET: api/BenefitPlanType/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BenefitPlanType>> GetBenefitPlanType(int id)
        {
            var benefitPlanType = await repository.GetById(id);
            if (benefitPlanType == null)
            {
                return NotFound();
            }
            return Ok(benefitPlanType);
        }

        // PUT: api/BenefitPlanType/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBenefitPlanType(int id, BenefitPlanType benefitPlanType)
        {
            if (id != benefitPlanType.Id)
            {
                return BadRequest();
            }

            var updatedBenefitPlanType = await repository.Update(benefitPlanType, id);
            if (updatedBenefitPlanType == null)
            {
                return NotFound();
            }

            return Ok(updatedBenefitPlanType);
        }

        // POST: api/BenefitPlanType

        [HttpPost]
        public async Task<ActionResult<BenefitPlanType>> PostBenefitPlanType(BenefitPlanType benefitPlanType)
        {
            var addedBenefitPlanType = await repository.Add(benefitPlanType);
            if (addedBenefitPlanType == null)
            {
                return NotFound();
            }
            return Ok(addedBenefitPlanType);
        }

        // DELETE: api/BenefitPlanType/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBenefitPlanType(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }
    }
}
