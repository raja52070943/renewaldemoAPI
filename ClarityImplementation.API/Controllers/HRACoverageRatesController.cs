using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models.Plans.COBRA;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HRACoverageRatesController : ControllerBase
    {
        private readonly GenericRepository<HRACoverageRate> repository;
        public HRACoverageRatesController(ClarityDbContext context)
        {
            repository = new GenericRepository<HRACoverageRate>(context);
        }
        // GET: api/HRACoverageRates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HRACoverageRate>>> GetHRACoverageRates()
        {
            var HRACoverageRates = await repository.GetAll();
            if (HRACoverageRates == null)
            {
                return NotFound();
            }
            return Ok(HRACoverageRates);
        }

        // GET: api/HRACoverageRates/5
        [HttpGet("{id}")]
        //[HttpGet("{id}")]
        public async Task<ActionResult<HRACoverageRate>> GetHRACoverageRate(int id)
        {
            var HRACoverageRate = await repository.GetById(id);
            if (HRACoverageRate == null)
            {
                return NotFound();
            }
            return Ok(HRACoverageRate);
        }


        //[HttpGet("{id}")]
        [HttpGet("ByCOBRAHRAPlanId/{id}")]
        public async Task<ActionResult<HRACoverageRate>> GetHRACoverageRates(int id)
        {
            var HRACoverageRates = await repository.GetAllByCompanyId(entity => entity.HRAPlanId == id);
            if (HRACoverageRates == null)
            {
                return NotFound();
            }
            return Ok(HRACoverageRates);
        }

        // PUT: api/HRACoverageRates/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutHRACoverageRate(int id, HRACoverageRate HRACoverageRate)
        {
            if (id != HRACoverageRate.Id)
            {
                return BadRequest();
            }

            var updatedHRACoverageRate = await repository.Update(HRACoverageRate, id);
            if (updatedHRACoverageRate == null)
            {
                return NotFound();
            }
            return Ok(updatedHRACoverageRate);
        }

        // POST: api/HRACoverageRates

        [HttpPost]
        public async Task<ActionResult<HRACoverageRate>> PostHRACoverageRate(HRACoverageRate HRACoverageRate)
        {

            var addedHRACoverageRate = await repository.Add(HRACoverageRate);
            if (addedHRACoverageRate == null)
            {
                return NotFound();
            }
            return Ok(addedHRACoverageRate);
        }

        // DELETE: api/HRACoverageRates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHRACoverageRate(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }
    }
}
