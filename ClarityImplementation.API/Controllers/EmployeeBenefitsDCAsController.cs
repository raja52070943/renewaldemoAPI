using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models.Plans;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeBenefitsDCAsController : ControllerBase
    {
        private readonly GenericRepository<EmployeeBenefitsDCA> repository;
        public EmployeeBenefitsDCAsController(ClarityDbContext context)
        {
            repository = new GenericRepository<EmployeeBenefitsDCA>(context);
        }

        // GET: api/EmployeeBenefitsDCAs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeBenefitsDCA>>> GetEmployeeBenefitsDCAs()
        {
            var EmployeeBenefitsDCAs = await repository.GetAll();
            if (EmployeeBenefitsDCAs == null)
            {
                return NotFound();
            }
            return Ok(EmployeeBenefitsDCAs);
        }

        [HttpGet("{id}")]
        //[HttpGet("{id}")]
        public async Task<ActionResult<EmployeeBenefitsDCA>> GetEmployeeBenefitsDCA(int id)
        {
            var EmployeeBenefitsDCA = await repository.GetById(id);
            if (EmployeeBenefitsDCA == null)
            {
                return NotFound();
            }
            return Ok(EmployeeBenefitsDCA);
        }

        [HttpGet("ByEmployeeBenefitsFSAId/{id}")]
        public async Task<ActionResult<EmployeeBenefitsDCA>> GetEmployeeBenefitsDCAByFSAId(int id)
        {
            var employeeBenefitsDCA = await repository.GetByCompanyId(entity => entity.EmployeeBenefitsFSAId == id);
            if (employeeBenefitsDCA == null)
            {
                return NotFound();
            }
            return Ok(employeeBenefitsDCA);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeBenefitsDCA(int id, EmployeeBenefitsDCA EmployeeBenefitsDCA)
        {
            if (id != EmployeeBenefitsDCA.Id)
            {
                return BadRequest();
            }

            var updatedEmployeeBenefitsDCA = await repository.Update(EmployeeBenefitsDCA, id);
            if (updatedEmployeeBenefitsDCA == null)
            {
                return NotFound();
            }
            return Ok(updatedEmployeeBenefitsDCA);
        }

        [HttpPost]
        public async Task<ActionResult<IActionResult>> PostEmployeeBenefitsDCA(EmployeeBenefitsDCA EmployeeBenefitsDCA)
        {

            var addedEmployeeBenefitsDCA = await repository.Add(EmployeeBenefitsDCA);
            if (addedEmployeeBenefitsDCA == null)
            {
                return NotFound();
            }
            return Ok(addedEmployeeBenefitsDCA);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeBenefitsDCA(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }
    }
}
