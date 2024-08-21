using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models.Plans.COBRA;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsuranceCoverageRatesController : ControllerBase
    {
        private readonly GenericRepository<InsurancePlanCoverageRate> repository;
        public InsuranceCoverageRatesController(ClarityDbContext context)
        {
            repository = new GenericRepository<InsurancePlanCoverageRate>(context);
        }
        // GET: api/InsurancePlanCoverageRates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InsurancePlanCoverageRate>>> GetInsurancePlanCoverageRates()
        {
            var insurancePlanCoverageRates = await repository.GetAll();
            if (insurancePlanCoverageRates == null)
            {
                return NotFound();
            }
            return Ok(insurancePlanCoverageRates);
        }

        // GET: api/InsurancePlanCoverageRates/5
        [HttpGet("{id}")]
        //[HttpGet("{id}")]
        public async Task<ActionResult<InsurancePlanCoverageRate>> GetInsurancePlanCoverageRate(int id)
        {
            var insurancePlanCoverageRate = await repository.GetById(id);
            if (insurancePlanCoverageRate == null)
            {
                return NotFound();
            }
            return Ok(insurancePlanCoverageRate);
        }


        //[HttpGet("{id}")]
        [HttpGet("ByCOBRAInsurancePlanId/{id}")]
        public async Task<ActionResult<InsurancePlanCoverageRate>> GetInsurancePlanCoverageRates(int id)
        {
            var insurancePlanCoverageRates = await repository.GetAllByCompanyId(entity => entity.InsurancePlanId == id);
            if (insurancePlanCoverageRates == null)
            {
                return NotFound();
            }
            return Ok(insurancePlanCoverageRates);
        }

        // PUT: api/InsurancePlanCoverageRates/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutInsurancePlanCoverageRate(int id, InsurancePlanCoverageRate insurancePlanCoverageRate)
        {
            if (id != insurancePlanCoverageRate.Id)
            {
                return BadRequest();
            }

            var updatedInsurancePlanCoverageRate = await repository.Update(insurancePlanCoverageRate, id);
            if (updatedInsurancePlanCoverageRate == null)
            {
                return NotFound();
            }
            return Ok(updatedInsurancePlanCoverageRate);
        }

        // POST: api/InsurancePlanCoverageRates

        [HttpPost]
        public async Task<ActionResult<InsurancePlanCoverageRate>> PostInsurancePlanCoverageRate(InsurancePlanCoverageRate insurancePlanCoverageRate)
        {

            var addedInsurancePlanCoverageRate = await repository.Add(insurancePlanCoverageRate);
            if (addedInsurancePlanCoverageRate == null)
            {
                return NotFound();
            }
            return Ok(addedInsurancePlanCoverageRate);
        }

        // DELETE: api/InsurancePlanCoverageRates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInsurancePlanCoverageRate(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }
    }
}
