using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models.Plans;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrolledEmployeesController : Controller
    {
        private readonly GenericRepository<EnrolledEmployee> repository;

        public EnrolledEmployeesController(ClarityDbContext context)
        {
            repository = new GenericRepository<EnrolledEmployee>(context);
        }

        // GET: api/InformationProviders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnrolledEmployee>>> GetEnrolledEmployees()
        {
            var enrolledEmployees = await repository.GetAll();
            if (enrolledEmployees == null)
            {
                return NotFound();
            }
            return Ok(enrolledEmployees);
        }

        // GET: api/InformationProviders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EnrolledEmployee>> GetEnrolledEmployee(int id)
        {
            var enrolledEmployee = await repository.GetById(id);
            if (enrolledEmployee == null)
            {
                return NotFound();
            }
            return Ok(enrolledEmployee);
        }

        // PUT: api/InformationProvider/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEnrolledEmployee(int id, EnrolledEmployee enrolledEmployee)
        {
            if (id != enrolledEmployee.Id)
            {
                return BadRequest();
            }

            var updatedEnrolledEmployee = await repository.Update(enrolledEmployee, id);
            if (updatedEnrolledEmployee == null)
            {
                return NotFound();
            }

            return Ok(updatedEnrolledEmployee);
        }

        // POST: api/InformationProviders

        [HttpPost]
        public async Task<ActionResult<EnrolledEmployee>> PostEnrolledEmployee(EnrolledEmployee enrolledEmployee)
        {
            var addedEnrolledEmployee = await repository.Add(enrolledEmployee);
            if (addedEnrolledEmployee == null)
            {
                return NotFound();
            }
            return Ok(addedEnrolledEmployee);
        }

        // DELETE: api/InformationProviders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnrolledEmployee(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }
    }
}
