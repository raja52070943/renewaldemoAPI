using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models.Plans;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeGroupsController : Controller
    {
        private readonly GenericRepository<EmployeeGroup> repository;

        public EmployeeGroupsController(ClarityDbContext context)
        {
            repository = new GenericRepository<EmployeeGroup>(context);
        }

        // GET: api/EmployeeGroup
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeGroup>>> GetEmployeeGroups()
        {
            var employeeGroups = await repository.GetAll();
            if (employeeGroups == null)
            {
                return NotFound();
            }
            return Ok(employeeGroups);
        }

        // GET: api/EmployeeGroups/5
        [HttpGet("{id}")]
        //[HttpGet("{id}")]
        public async Task<ActionResult<EmployeeGroup>> GetEmployeeGroup(int id)
        {
            var employeeGroup = await repository.GetById(id);
            if (employeeGroup == null)
            {
                return NotFound();
            }
            return Ok(employeeGroup);
        }

        // PUT: api/EmployeeGroups/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEBPlan(int id, EmployeeGroup employeeGroup)
        {
            if (id != employeeGroup.Id)
            {
                return BadRequest();
            }

            var updatedEmployeeGroup = await repository.Update(employeeGroup, id);
            if (updatedEmployeeGroup == null)
            {
                return NotFound();
            }
            return Ok(updatedEmployeeGroup);
        }

        // POST: api/EmployeeGroup

        [HttpPost]
        public async Task<ActionResult<EBPlan>> PostEBPlan(EmployeeGroup employeeGroup)
        {
            var addedEmployeeGroup = await repository.Add(employeeGroup);
            if (addedEmployeeGroup == null)
            {
                return NotFound();
            }
            return Ok(addedEmployeeGroup);
        }


        // DELETE: api/EmployeeGroup/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeGroup(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }
    }
}
