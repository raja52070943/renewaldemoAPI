using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models.Plans;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeContributionGroupsController : Controller
    {
        private readonly GenericRepository<EmployeeContributionGroup> repository;
        private readonly GenericRepository<EmployeeBenefitsHSA> hsaRepo;
        public EmployeeContributionGroupsController(ClarityDbContext context)
        {
            repository = new GenericRepository<EmployeeContributionGroup>(context);
            hsaRepo = new GenericRepository<EmployeeBenefitsHSA>(context);
        }

        // GET: api/MidYearPlans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeContributionGroup>>> GetEmployeeContributionGroups()
        {
            var employeeContributionGroups = await repository.GetAll();
            if (employeeContributionGroups == null)
            {
                return NotFound();
            }
            return Ok(employeeContributionGroups);
        }

        // GET: api/MidYearPlans/5
        [HttpGet("{id}")]
        //[HttpGet("{id}")]
        public async Task<ActionResult<EmployeeBenefitsHSA>> GetEmployeeContributionGroup(int id)
        {
            var employeeContributionGroup = await repository.GetById(id);
            if (employeeContributionGroup == null)
            {
                return NotFound();
            }

            return Ok(employeeContributionGroup);
        }

        //[HttpGet("{id}")]
        [HttpGet("ByEmployeeBenefitsHSAId/{id}")]
        public async Task<ActionResult<EmployeeContributionGroup>> GetEmployeeContributionGroupByHSAId(int id)
        {
            var employeeContributionGroups = await repository.GetAllByCompanyId(entity => entity.EmployeeBenefitsHSAId == id);
            if (employeeContributionGroups == null)
            {
                return NotFound();
            }
            return Ok(employeeContributionGroups);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeContributionGroup(int id, EmployeeContributionGroup employeeContributionGroup)
        {
            if (id != employeeContributionGroup.Id)
            {
                return BadRequest();
            }

            var updatedEmployeeContributionGroup = await repository.Update(employeeContributionGroup, id);
            if (updatedEmployeeContributionGroup == null)
            {
                return NotFound();
            }
            return Ok(updatedEmployeeContributionGroup);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeContributionGroup>> PostEmployeeContributionGroup(EmployeeContributionGroup employeeContributionGroup)
        {
            var addedEmployeeContributionGroup = await repository.Add(employeeContributionGroup);
            if (addedEmployeeContributionGroup == null)
            {
                return NotFound();
            }
            var existingHSA = await hsaRepo.GetById(employeeContributionGroup.EmployeeBenefitsHSAId);
            existingHSA.IsEmployerContribution = "true";
            await hsaRepo.Update(existingHSA, employeeContributionGroup.EmployeeBenefitsHSAId);
            return Ok(addedEmployeeContributionGroup);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeContributionGroup(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }

        // DELETE: api/ByEmpl/5
        [HttpDelete("ByEmployeeBenefitsHSAId/{id}")]
        public async Task<IActionResult> DeleteAllByHSAId(int id)
        {
            var deletedResponse = await repository.DeleteByCompanyId(entity => entity.EmployeeBenefitsHSAId == id);
            var existingHSA = await hsaRepo.GetById(id);
            existingHSA.IsEmployerContribution = "false";
            await hsaRepo.Update(existingHSA, id);
            return Ok(deletedResponse);
        }
    }
}
