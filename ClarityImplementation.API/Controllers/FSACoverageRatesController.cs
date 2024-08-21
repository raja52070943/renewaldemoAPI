using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models.Plans.COBRA;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FSACoverageRatesController : ControllerBase
    {
        private readonly GenericRepository<FSACoverageRate> repository;
        public FSACoverageRatesController(ClarityDbContext context)
        {
            repository = new GenericRepository<FSACoverageRate>(context);
        }
        // GET: api/FSACoverageRates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FSACoverageRate>>> GetFSACoverageRates()
        {
            var FSACoverageRates = await repository.GetAll();
            if (FSACoverageRates == null)
            {
                return NotFound();
            }
            return Ok(FSACoverageRates);
        }

        // GET: api/FSACoverageRates/5
        [HttpGet("{id}")]
        //[HttpGet("{id}")]
        public async Task<ActionResult<FSACoverageRate>> GetFSACoverageRate(int id)
        {
            var FSACoverageRate = await repository.GetById(id);
            if (FSACoverageRate == null)
            {
                return NotFound();
            }
            return Ok(FSACoverageRate);
        }

        //[HttpGet("{id}")]
        [HttpGet("ByCOBRAFSAPlanId/{id}")]
        public async Task<ActionResult<FSACoverageRate>> GetFSACoverageRates(int id)
        {
            var FSACoverageRates = await repository.GetAllByCompanyId(entity => entity.FSAPlanId == id);
            if (FSACoverageRates == null)
            {
                return NotFound();
            }
            return Ok(FSACoverageRates);
        }

        // PUT: api/FSACoverageRates/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutFSACoverageRate(int id, FSACoverageRate FSACoverageRate)
        {
            if (id != FSACoverageRate.Id)
            {
                return BadRequest();
            }

            var updatedFSACoverageRate = await repository.Update(FSACoverageRate, id);
            if (updatedFSACoverageRate == null)
            {
                return NotFound();
            }
            return Ok(updatedFSACoverageRate);
        }

        // POST: api/FSACoverageRates

        [HttpPost]
        public async Task<ActionResult<FSACoverageRate>> PostFSACoverageRate(FSACoverageRate FSACoverageRate)
        {

            var addedFSACoverageRate = await repository.Add(FSACoverageRate);
            if (addedFSACoverageRate == null)
            {
                return NotFound();
            }
            return Ok(addedFSACoverageRate);
        }

        // DELETE: api/FSACoverageRates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFSACoverageRate(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }
    }
}
