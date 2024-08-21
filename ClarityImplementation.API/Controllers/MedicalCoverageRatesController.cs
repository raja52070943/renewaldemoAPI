using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models.Plans;
using ClarityImplementation.API.Models.Plans.COBRA;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalCoverageRatesController : ControllerBase
    {
        private readonly GenericRepository<MedicalPlanCoverageRate> repository;
        private readonly GenericRepository<COBRAMedicalPlan> cobraMedicalPlanRepository;
        public MedicalCoverageRatesController(ClarityDbContext context)
        {
            repository = new GenericRepository<MedicalPlanCoverageRate>(context);
            cobraMedicalPlanRepository = new GenericRepository<COBRAMedicalPlan>(context);

        }
        // GET: api/MedicalPlanCoverageRates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicalPlanCoverageRate>>> GetMedicalPlanCoverageRates()
        {
            var medicalPlanCoverageRates = await repository.GetAll();
            if (medicalPlanCoverageRates == null)
            {
                return NotFound();
            }
            return Ok(medicalPlanCoverageRates);
        }

        // GET: api/MedicalPlanCoverageRates/5
        [HttpGet("{id}")]
        //[HttpGet("{id}")]
        public async Task<ActionResult<MedicalPlanCoverageRate>> GetMedicalPlanCoverageRate(int id)
        {
            var medicalPlanCoverageRate = await repository.GetById(id);
            if (medicalPlanCoverageRate == null)
            {
                return NotFound();
            }
            return Ok(medicalPlanCoverageRate);
        }


        //[HttpGet("{id}")]
        [HttpGet("ByCOBRAMedicalPlanId/{id}")]
        public async Task<ActionResult<MedicalPlanCoverageRate>> GetMedicalPlanCoverageRates(int id)
        {
            var medicalPlanCoverageRates = await repository.GetAllByCompanyId(entity => entity.COBRAMedicalPlanId == id);
            if (medicalPlanCoverageRates == null)
            {
                return NotFound();
            }
            return Ok(medicalPlanCoverageRates);
        }

        // PUT: api/MedicalPlanCoverageRates/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedicalPlanCoverageRate(int id, MedicalPlanCoverageRate medicalPlanCoverageRate)
        {
            if (id != medicalPlanCoverageRate.Id)
            {
                return BadRequest();
            }

            var updatedMedicalPlanCoverageRate = await repository.Update(medicalPlanCoverageRate, id);
            if (updatedMedicalPlanCoverageRate == null)
            {
                return NotFound();
            }
            return Ok(updatedMedicalPlanCoverageRate);
        }

        // POST: api/MedicalPlanCoverageRates

        [HttpPost]
        public async Task<ActionResult<MedicalPlanCoverageRate>> PostMedicalPlanCoverageRate(MedicalPlanCoverageRate medicalPlanCoverageRate)
        {

            var addedMedicalPlanCoverageRate = await repository.Add(medicalPlanCoverageRate);
            if (addedMedicalPlanCoverageRate == null)
            {
                return NotFound();
            }
            return Ok(addedMedicalPlanCoverageRate);
        }

        // DELETE: api/MedicalPlanCoverageRates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicalPlanCoverageRate(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }
    }
}
