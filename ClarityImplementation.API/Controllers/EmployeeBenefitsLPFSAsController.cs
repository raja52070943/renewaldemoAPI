using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models.Plans;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeBenefitsLPFSAsController : ControllerBase
    {
        private readonly GenericRepository<EmployeeBenefitsLPFSA> repository;
        public EmployeeBenefitsLPFSAsController(ClarityDbContext context)
        {
            repository = new GenericRepository<EmployeeBenefitsLPFSA>(context);
        }

        // GET: api/EmployeeBenefitsLPFSAs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeBenefitsLPFSA>>> GetEmployeeBenefitsLPFSAs()
        {
            var employeeBenefitsLPFSAs = await repository.GetAll();
            if (employeeBenefitsLPFSAs == null)
            {
                return NotFound();
            }
            return Ok(employeeBenefitsLPFSAs);
        }

        [HttpGet("ByEmployeeBenefitsFSAId/{id}")]
        public async Task<ActionResult<EmployeeBenefitsLPFSA>> GetEmployeeBenefitsLPFSAByFSAId(int id)
        {
            var employeeBenefitsLPFSA = await repository.GetByCompanyId(entity => entity.EmployeeBenefitsFSAId == id);
            if (employeeBenefitsLPFSA == null)
            {
                return NotFound();
            }
            return Ok(employeeBenefitsLPFSA);
        }

        [HttpGet("{id}")]
        //[HttpGet("{id}")]
        public async Task<ActionResult<EmployeeBenefitsLPFSA>> GetEmployeeBenefitsLPFSA(int id)
        {
            var employeeBenefitsLPFSA = await repository.GetById(id);
            if (employeeBenefitsLPFSA == null)
            {
                return NotFound();
            }
            return Ok(employeeBenefitsLPFSA);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeBenefitsLPFSA(int id, EmployeeBenefitsLPFSA employeeBenefitsLPFSA)
        {
            if (id != employeeBenefitsLPFSA.Id)
            {
                return BadRequest();
            }

            var updatedEmployeeBenefitsLPFSA = await repository.Update(employeeBenefitsLPFSA, id);
            if (updatedEmployeeBenefitsLPFSA == null)
            {
                return NotFound();
            }
            return Ok(updatedEmployeeBenefitsLPFSA);
        }

        [HttpPost]
        public async Task<ActionResult<IActionResult>> PostEmployeeBenefitsLPFSA(EmployeeBenefitsLPFSA employeeBenefitsLPFSA)
        {

            var addedEmployeeBenefitsLPFSA = await repository.Add(employeeBenefitsLPFSA);
            if (addedEmployeeBenefitsLPFSA == null)
            {
                return NotFound();
            }
            return Ok(addedEmployeeBenefitsLPFSA);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeBenefitsLPFSA(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }
    }
}
