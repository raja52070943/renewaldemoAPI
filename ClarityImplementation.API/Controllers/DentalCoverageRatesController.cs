using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models.Plans.COBRA;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DentalCoverageRatesController : ControllerBase
    {
        private readonly GenericRepository<DentalCoverageRate> repository;
        public DentalCoverageRatesController(ClarityDbContext context)
        {
            repository = new GenericRepository<DentalCoverageRate>(context);
        }
        // GET: api/DentalCoverageRates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DentalCoverageRate>>> GetDentalCoverageRates()
        {
            var dentalCoverageRates = await repository.GetAll();
            if (dentalCoverageRates == null)
            {
                return NotFound();
            }
            return Ok(dentalCoverageRates);
        }

        // GET: api/DentalCoverageRates/5
        [HttpGet("{id}")]
        //[HttpGet("{id}")]
        public async Task<ActionResult<DentalCoverageRate>> GetDentalCoverageRate(int id)
        {
            var dentalCoverageRate = await repository.GetById(id);
            if (dentalCoverageRate == null)
            {
                return NotFound();
            }
            return Ok(dentalCoverageRate);
        }


        //[HttpGet("{id}")]
        [HttpGet("ByCOBRADentalPlanId/{id}")]
        public async Task<ActionResult<DentalCoverageRate>> GetDentalCoverageRates(int id)
        {
            var dentalCoverageRates = await repository.GetAllByCompanyId(entity => entity.DentalPlanId == id);
            if (dentalCoverageRates == null)
            {
                return NotFound();
            }
            return Ok(dentalCoverageRates);
        }

        // PUT: api/DentalCoverageRates/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDentalCoverageRate(int id, DentalCoverageRate dentalCoverageRate)
        {
            if (id != dentalCoverageRate.Id)
            {
                return BadRequest();
            }

            var updatedDentalCoverageRate = await repository.Update(dentalCoverageRate, id);
            if (updatedDentalCoverageRate == null)
            {
                return NotFound();
            }
            return Ok(updatedDentalCoverageRate);
        }

        // POST: api/DentalCoverageRates

        [HttpPost]
        public async Task<ActionResult<DentalCoverageRate>> PostDentalCoverageRate(DentalCoverageRate dentalCoverageRate)
        {

            var addedDentalCoverageRate = await repository.Add(dentalCoverageRate);
            if (addedDentalCoverageRate == null)
            {
                return NotFound();
            }
            return Ok(addedDentalCoverageRate);
        }

        // DELETE: api/DentalCoverageRates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDentalCoverageRate(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }
    }
}
