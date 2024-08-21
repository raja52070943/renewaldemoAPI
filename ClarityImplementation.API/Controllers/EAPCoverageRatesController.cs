using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models.Plans.COBRA;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EAPCoverageRatesController : Controller
    {
        private readonly GenericRepository<EAPCoverageRate> repository;
        public EAPCoverageRatesController(ClarityDbContext context)
        {
            repository = new GenericRepository<EAPCoverageRate>(context);
        }
        // GET: api/EAPCoverageRates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EAPCoverageRate>>> GetEAPCoverageRates()
        {
            var EAPCoverageRates = await repository.GetAll();
            if (EAPCoverageRates == null)
            {
                return NotFound();
            }
            return Ok(EAPCoverageRates);
        }

        // GET: api/EAPCoverageRates/5
        [HttpGet("{id}")]
        //[HttpGet("{id}")]
        public async Task<ActionResult<EAPCoverageRate>> GetEAPCoverageRate(int id)
        {
            var EAPCoverageRate = await repository.GetById(id);
            if (EAPCoverageRate == null)
            {
                return NotFound();
            }
            return Ok(EAPCoverageRate);
        }

        //[HttpGet("{id}")]
        [HttpGet("ByCOBRAEAPPlanId/{id}")]
        public async Task<ActionResult<EAPCoverageRate>> GetEAPCoverageRates(int id)
        {
            var EAPCoverageRates = await repository.GetAllByCompanyId(entity => entity.EAPPlanId == id);
            if (EAPCoverageRates == null)
            {
                return NotFound();
            }
            return Ok(EAPCoverageRates);
        }

        // PUT: api/EAPCoverageRates/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEAPCoverageRate(int id, EAPCoverageRate EAPCoverageRate)
        {
            if (id != EAPCoverageRate.Id)
            {
                return BadRequest();
            }

            var updatedEAPCoverageRate = await repository.Update(EAPCoverageRate, id);
            if (updatedEAPCoverageRate == null)
            {
                return NotFound();
            }
            return Ok(updatedEAPCoverageRate);
        }

        // POST: api/EAPCoverageRates

        [HttpPost]
        public async Task<ActionResult<EAPCoverageRate>> PostEAPCoverageRate(EAPCoverageRate EAPCoverageRate)
        {

            var addedEAPCoverageRate = await repository.Add(EAPCoverageRate);
            if (addedEAPCoverageRate == null)
            {
                return NotFound();
            }
            return Ok(addedEAPCoverageRate);
        }

        // DELETE: api/EAPCoverageRates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEAPCoverageRate(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }
    }
}
