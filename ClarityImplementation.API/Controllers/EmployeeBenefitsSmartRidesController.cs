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
    public class EmployeeBenefitsSmartRidesController : ControllerBase
    {
        private readonly GenericRepository<EmployeeBenefitsSmartRide> repository;
        public EmployeeBenefitsSmartRidesController(ClarityDbContext context)
        {
            repository = new GenericRepository<EmployeeBenefitsSmartRide>(context);
        }

        // GET: api/EmployeeBenefitsSmartRides
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeBenefitsSmartRide>>> GetEmployeeBenefitsSmartRides()
        {
            var employeeBenefitsSmartRides = await repository.GetAll();
            if (employeeBenefitsSmartRides == null)
            {
                return NotFound();
            }
            return Ok(employeeBenefitsSmartRides);
        }

        [HttpGet("ByEmployeeBenefitsPlanId/{id}")]
        public async Task<ActionResult<EmployeeBenefitsSmartRide>> GetEmployeeBenefitsSmartRideByPlanId(int id)
        {
            var employeeBenefitsSmartRide = await repository.GetByCompanyId(entity => entity.EmployeeBenefitsPlanId == id);
            if (employeeBenefitsSmartRide == null)
            {
                return NotFound();
            }
            return Ok(employeeBenefitsSmartRide);
        }

        [HttpGet("{id}")]
        //[HttpGet("{id}")]
        public async Task<ActionResult<EmployeeBenefitsSmartRide>> GetEmployeeBenefitsSmartRide(int id)
        {
            var employeeBenefitsSmartRide = await repository.GetById(id);
            if (employeeBenefitsSmartRide == null)
            {
                return NotFound();
            }
            return Ok(employeeBenefitsSmartRide);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeBenefitsSmartRide(int id, EmployeeBenefitsSmartRide employeeBenefitsSmartRide)
        {
            if (id != employeeBenefitsSmartRide.Id)
            {
                return BadRequest();
            }

            var updatedEmployeeBenefitsSmartRide = await repository.Update(employeeBenefitsSmartRide, id);
            if (updatedEmployeeBenefitsSmartRide == null)
            {
                return NotFound();
            }
            return Ok(updatedEmployeeBenefitsSmartRide);
        }

        [HttpPost]
        public async Task<ActionResult<IActionResult>> PostEmployeeBenefitsSmartRide(EmployeeBenefitsSmartRide employeeBenefitsSmartRide)
        {

            var addedEmployeeBenefitsSmartRide = await repository.Add(employeeBenefitsSmartRide);
            if (addedEmployeeBenefitsSmartRide == null)
            {
                return NotFound();
            }
            return Ok(addedEmployeeBenefitsSmartRide);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeBenefitsSmartRide(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }

    }
}
