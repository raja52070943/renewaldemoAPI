using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models;
using ClarityImplementation.API.Models.Plans.COBRA;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisionCoverageRatesController : ControllerBase
    {
        private readonly GenericRepository<VisionCoverageRate> repository;
        public VisionCoverageRatesController(ClarityDbContext context)
        {
            repository = new GenericRepository<VisionCoverageRate>(context);
        }
        // GET: api/VisionCoverageRates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VisionCoverageRate>>> GetVisionCoverageRates()
        {
            var visionCoverageRates = await repository.GetAll();
            if (visionCoverageRates == null)
            {
                return NotFound();
            }
            return Ok(visionCoverageRates);
        }

        // GET: api/VisionCoverageRates/5
        [HttpGet("{id}")]
        //[HttpGet("{id}")]
        public async Task<ActionResult<VisionCoverageRate>> GetVisionCoverageRate(int id)
        {
            var visionCoverageRate = await repository.GetById(id);
            if (visionCoverageRate == null)
            {
                return NotFound();
            }
            return Ok(visionCoverageRate);
        }

        
        //[HttpGet("{id}")]
        [HttpGet("ByCOBRAVisionPlanId/{id}")]
        public async Task<ActionResult<VisionCoverageRate>> GetVisionCoverageRates(int id)
        {
            var visionCoverageRates = await repository.GetAllByCompanyId(entity => entity.VisionPlanId == id);
            if (visionCoverageRates == null)
            {
                return NotFound();
            }
            return Ok(visionCoverageRates);
        }

        // PUT: api/VisionCoverageRates/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutVisionCoverageRate(int id, VisionCoverageRate visionCoverageRate)
        {
            if (id != visionCoverageRate.Id)
            {
                return BadRequest();
            }

            var updatedVisionCoverageRate = await repository.Update(visionCoverageRate, id);
            if (updatedVisionCoverageRate == null)
            {
                return NotFound();
            }
            return Ok(updatedVisionCoverageRate);
        }

        // POST: api/VisionCoverageRates

        [HttpPost]
        public async Task<ActionResult<VisionCoverageRate>> PostVisionCoverageRate(VisionCoverageRate visionCoverageRate)
        {
            
            var addedVisionCoverageRate = await repository.Add(visionCoverageRate);
            if (addedVisionCoverageRate == null)
            {
                return NotFound();
            }
            return Ok(addedVisionCoverageRate);
        }

        // DELETE: api/VisionCoverageRates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVisionCoverageRate(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }

    }
}
