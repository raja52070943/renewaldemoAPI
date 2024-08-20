using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models.Plans;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EligibilityDefinitionsController : Controller
    {
        private readonly GenericRepository<EligibilityDefinition> repository;

        public EligibilityDefinitionsController(ClarityDbContext context)
        {
            repository = new GenericRepository<EligibilityDefinition>(context);
        }

        // GET: api/EligibilityDefinitions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EligibilityDefinition>>> GetEligibilityDefinitions()
        {
            var eligibilityDefinitions = await repository.GetAll();
            if (eligibilityDefinitions == null)
            {
                return NotFound();
            }
            return Ok(eligibilityDefinitions);
        }

        // GET: api/EligibilityDefinitions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EligibilityDefinition>> GetEligibilityDefinition(int id)
        {
            var eligibilityDefinition = await repository.GetById(id);
            if (eligibilityDefinition == null)
            {
                return NotFound();
            }
            return Ok(eligibilityDefinition);
        }

        // PUT: api/EligibilityDefinition/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEligibilityDefinition(int id, EligibilityDefinition eligibilityDefinition)
        {
            if (id != eligibilityDefinition.Id)
            {
                return BadRequest();
            }

            var updatedEligibilityDefinition = await repository.Update(eligibilityDefinition, id);
            if (updatedEligibilityDefinition == null)
            {
                return NotFound();
            }

            return Ok(updatedEligibilityDefinition);
        }

        // POST: api/EligibilityDefinition

        [HttpPost]
        public async Task<ActionResult<EligibilityDefinition>> PostEligibilityDefinition(EligibilityDefinition eligibilityDefinition)
        {
            var addedEligibilityDefinition = await repository.Add(eligibilityDefinition);
            if (addedEligibilityDefinition == null)
            {
                return NotFound();
            }
            return Ok(addedEligibilityDefinition);
        }

        // DELETE: api/EligibilityDefinition/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEligibilityDefinition(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }
    }

}