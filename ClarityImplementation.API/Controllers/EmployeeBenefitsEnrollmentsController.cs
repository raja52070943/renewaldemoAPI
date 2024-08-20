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
    public class EmployeeBenefitsEnrollmentsController : ControllerBase
    {
        private readonly GenericRepository<EmployeeBenefitsEnrollment> repository;
        public EmployeeBenefitsEnrollmentsController(ClarityDbContext context)
        {
            repository = new GenericRepository<EmployeeBenefitsEnrollment>(context);
        }

        // GET: api/EmployeeBenefitsEnrollments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeBenefitsEnrollment>>> GetEmployeeBenefitsEnrollments()
        {
            var employeeBenefitsEnrollments = await repository.GetAll();
            if (employeeBenefitsEnrollments == null)
            {
                return NotFound();
            }
            return Ok(employeeBenefitsEnrollments);
        }

        [HttpGet("ByEmployeeBenefitsPlanId/{id}")]
        public async Task<ActionResult<EmployeeBenefitsEnrollment>> GetEmployeeBenefitsPlanEnrollment(int id)
        {
            var employeeBenefitsEnrollment = await repository.GetByCompanyId(entity => entity.EmployeeBenefitsPlanId == id);
            if (employeeBenefitsEnrollment == null)
            {
                return NotFound();
            }
            return Ok(employeeBenefitsEnrollment);
        }


        [HttpGet("{id}")]
        //[HttpGet("{id}")]
        public async Task<ActionResult<EmployeeBenefitsEnrollment>> GetEmployeeBenefitsEnrollment(int id)
        {
            var employeeBenefitsEnrollment = await repository.GetById(id);
            if (employeeBenefitsEnrollment == null)
            {
                return NotFound();
            }
            return Ok(employeeBenefitsEnrollment);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeBenefitsEnrollment(int id, EmployeeBenefitsEnrollment employeeBenefitsEnrollment)
        {
            if (id != employeeBenefitsEnrollment.Id)
            {
                return BadRequest();
            }

            var updatedEmployeeBenefitsEnrollment = await repository.Update(employeeBenefitsEnrollment, id);
            if (updatedEmployeeBenefitsEnrollment == null)
            {
                return NotFound();
            }
            return Ok(updatedEmployeeBenefitsEnrollment);
        }

        [HttpPost]
        public async Task<ActionResult<IActionResult>> PostEmployeeBenefitsEnrollment(EmployeeBenefitsEnrollment employeeBenefitsEnrollment)
        {
            var addedEmployeeBenefitsEnrollment = await repository.Add(employeeBenefitsEnrollment);
            if (addedEmployeeBenefitsEnrollment == null)
            {
                return NotFound();
            }
            return Ok(addedEmployeeBenefitsEnrollment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeBenefitsEnrollment(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }
    }
}
