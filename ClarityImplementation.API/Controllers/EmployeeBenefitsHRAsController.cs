using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models.Plans;
using ClarityImplementation.API.Models;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeBenefitsHRAsController : ControllerBase
    {
        private readonly GenericRepository<EmployeeBenefitsHRA> repository;
        public EmployeeBenefitsHRAsController(ClarityDbContext context)
        {
            repository = new GenericRepository<EmployeeBenefitsHRA>(context);
        }

        // GET: api/EmployeeBenefitsHRAs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeBenefitsHRA>>> GetEmployeeBenefitsHRAs()
        {
            var employeeBenefitsHRAs = await repository.GetAll();
            if (employeeBenefitsHRAs == null)
            {
                return NotFound();
            }
            return Ok(employeeBenefitsHRAs);
        }

        [HttpGet("ByEmployeeBenefitsPlanId/{id}")]
        public async Task<ActionResult<EmployeeBenefitsHRA>> GetEmployeeBenefitsHRAByPlanId(int id)
        {
            var employeeBenefitsHRAs = await repository.GetAllByCompanyId(entity => entity.EmployeeBenefitsPlanId == id);
            if (employeeBenefitsHRAs == null)
            {
                return NotFound();
            }
            foreach (var employeeBenefitsHRA in employeeBenefitsHRAs)
            {
                if (employeeBenefitsHRA.ReimbursableExpenses != null)
                {
                    int[] selectedHRAReimbursableExpenses = Array.ConvertAll(employeeBenefitsHRA.ReimbursableExpenses.Split(','), r => int.Parse(r));
                    employeeBenefitsHRA.SelectedReimbursableExpenses = selectedHRAReimbursableExpenses.OfType<int>().ToList();

                }
            }
            return Ok(employeeBenefitsHRAs);
        }

        [HttpGet("{id}")]
        //[HttpGet("{id}")]
        public async Task<ActionResult<EmployeeBenefitsHRA>> GetEmployeeBenefitsHRA(int id)
        {
            var employeeBenefitsHRA = await repository.GetById(id);
            if (employeeBenefitsHRA == null)
            {
                return NotFound();
            }
            if (employeeBenefitsHRA.ReimbursableExpenses != null)
            {
                int[] selectedHRAReimbursableExpenses = Array.ConvertAll(employeeBenefitsHRA.ReimbursableExpenses.Split(','), r => int.Parse(r));
                employeeBenefitsHRA.SelectedReimbursableExpenses = selectedHRAReimbursableExpenses.OfType<int>().ToList();

            }
            return Ok(employeeBenefitsHRA);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeBenefitsHRA(int id, EmployeeBenefitsHRA employeeBenefitsHRA)
        {
            if (id != employeeBenefitsHRA.Id)
            {
                return BadRequest();
            }

            var updatedEmployeeBenefitsHRA = await repository.Update(employeeBenefitsHRA, id);
            if (updatedEmployeeBenefitsHRA == null)
            {
                return NotFound();
            }
            return Ok(updatedEmployeeBenefitsHRA);
        }

        [HttpPost]
        public async Task<ActionResult<IActionResult>> PostEmployeeBenefitsHRA(EmployeeBenefitsHRA employeeBenefitsHRA)
        {
            employeeBenefitsHRA.ReimbursableExpenses = "0";
            employeeBenefitsHRA.HRAUnusedFund = "newplan";
            employeeBenefitsHRA.IsHRAorFSA = "FSA";
            employeeBenefitsHRA.IsDependentCard = "true";
            employeeBenefitsHRA.HRAType = "Standard Integrated";
            employeeBenefitsHRA.ActiveEmployeeRunoutPeriod = "90 Days";
            employeeBenefitsHRA.TerminatedEmployeeRunoutPeriod = "90 Days";
            employeeBenefitsHRA.IsDependentCardOption = "Spouse Only";
            employeeBenefitsHRA.HRAUnusedFund = "Forfeited";
            var addedEmployeeBenefitsHRA = await repository.Add(employeeBenefitsHRA);
            if (addedEmployeeBenefitsHRA == null)
            {
                return NotFound();
            }
            return Ok(addedEmployeeBenefitsHRA);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeBenefitsHRA(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }
    }
}
