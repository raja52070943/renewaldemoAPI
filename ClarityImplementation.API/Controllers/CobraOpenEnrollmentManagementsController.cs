using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models.Plans.COBRA;
using ClarityImplementation.API.Models;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CobraOpenEnrollmentManagementsController : Controller
    {
        private readonly GenericRepository<CobraOpenEnrollmentManagement> repository;
        public CobraOpenEnrollmentManagementsController(ClarityDbContext context)
        {
            repository = new GenericRepository<CobraOpenEnrollmentManagement>(context);
        }

        // GET: api/CobraOpenEnrollmentManagement
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CobraOpenEnrollmentManagement>>> GetCobraOpenEnrollmentManagement()
        {
            var cobraOpenEnrollmentManagement = await repository.GetAll();
            if (cobraOpenEnrollmentManagement == null)
            {
                return NotFound();
            }
            return Ok(cobraOpenEnrollmentManagement);
        }

        // GET: api/CobraOpenEnrollmentManagement/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CobraOpenEnrollmentManagement>> GetCobraOpenEnrollmentManagementById(int id)
        {
            var cobraOpenEnrollmentManagement = await repository.GetById(id);
            if (cobraOpenEnrollmentManagement == null)
            {
                return NotFound();
            }

            return Ok(cobraOpenEnrollmentManagement);
        }

        // GET: api/CobraOpenEnrollmentManagement/5
        [HttpGet("ByPlanId/{id}")]
        public async Task<ActionResult<CobraOpenEnrollmentManagement>> GetCobraOpenEnrollmentManagementByPlan(int id)
        {
            var cobraOpenEnrollmentManagement = await repository.GetByCompanyId(entity => entity.COBRAPlanId == id);
            if (cobraOpenEnrollmentManagement == null)
            {
                return NotFound();
            }

            return Ok(cobraOpenEnrollmentManagement);
        }

        // PUT: api/CobraOpenEnrollmentManagement/5

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCobraOpenEnrollmentManagement(int id, CobraOpenEnrollmentManagement cobraOpenEnrollmentManagement)
        {
            if (id != cobraOpenEnrollmentManagement.Id)
            {
                return BadRequest();
            }

            var updatedCobraOpenEnrollmentManagement = await repository.Update(cobraOpenEnrollmentManagement, id);
            if (updatedCobraOpenEnrollmentManagement == null)
            {
                return NotFound();
            }

            return Ok(updatedCobraOpenEnrollmentManagement);
        }

        // POST: api/CobraOpenEnrollmentManagement

        [HttpPost]
        public async Task<ActionResult<Address>> PostCobraOpenEnrollmentManagement(CobraOpenEnrollmentManagement cobraOpenEnrollmentManagement)
        {
            var addedCobraOpenEnrollmentManagement = await repository.Add(cobraOpenEnrollmentManagement);
            if (addedCobraOpenEnrollmentManagement == null)
            {
                return NotFound();
            }
            return Ok(addedCobraOpenEnrollmentManagement);
        }


        // DELETE: api/CobraOpenEnrollmentManagement/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCobraOpenEnrollmentManagement(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }
    }
}
