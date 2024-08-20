using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models;
using ClarityImplementation.API.Models.Plans;
using ClarityImplementation.API.Models.Plans.COBRA;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Drawing2D;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CobraGeneralInformationsController : Controller
    {
        private readonly GenericRepository<CobraGeneralInformation> repository;

        public CobraGeneralInformationsController(ClarityDbContext context)
        {
            repository = new GenericRepository<CobraGeneralInformation>(context);
        }

        // GET: api/CobraGeneralInformation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CobraGeneralInformation>>> GetCobraGeneralInformation()
        {
            var cobraGeneralInformation = await repository.GetAll();
            if (cobraGeneralInformation == null)
            {
                return NotFound();
            }
            return Ok(cobraGeneralInformation);
        }

        // GET: api/CobraGeneralInformation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CobraGeneralInformation>> GetCobraGeneralInformationById(int id)
        {
            var cobraGeneralInformation = await repository.GetById(id);
            if (cobraGeneralInformation == null)
            {
                return NotFound();
            }
            
            return Ok(cobraGeneralInformation);
        }

        // GET: api/CobraGeneralInformation/5
        [HttpGet("ByPlanId/{id}")]
        public async Task<ActionResult<CobraGeneralInformation>> GetCobraGeneralInformationByPlan(int id)
        {
            var cobraGeneralInformation = await repository.GetByCompanyId(entity => entity.COBRAPlanId == id);
            if (cobraGeneralInformation == null)
            {
                return NotFound();
            }

            return Ok(cobraGeneralInformation);
        }

        // PUT: api/CobraGeneralInformation/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCobraGeneralInformation(int id, CobraGeneralInformation cobraGeneralInformation)
        {
            if (id != cobraGeneralInformation.Id)
            {
                return BadRequest();
            }

            var updatedCobraGeneralInformation = await repository.Update(cobraGeneralInformation, id);
            if (updatedCobraGeneralInformation == null)
            {
                return NotFound();
            }

            return Ok(updatedCobraGeneralInformation);
        }

        // POST: api/CobraGeneralInformation

        [HttpPost]
        public async Task<ActionResult<Address>> PostCobraGeneralInformation(CobraGeneralInformation cobraGeneralInformation)
        {
            var addedCobraGeneralInformation = await repository.Add(cobraGeneralInformation);
            if (addedCobraGeneralInformation == null)
            {
                return NotFound();
            }
            return Ok(addedCobraGeneralInformation);
        }


        // DELETE: api/CobraGeneralInformation/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCobraGeneralInformation(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }

    }
}
