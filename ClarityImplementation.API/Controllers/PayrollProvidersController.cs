using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models.Plans;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayrollProvidersController : Controller
    {
        private readonly GenericRepository<PayrollProvider> repository;

        public PayrollProvidersController(ClarityDbContext context)
        {
            repository = new GenericRepository<PayrollProvider>(context);
        }

        // GET: api/PayrollProviders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PayrollProvider>>> GetPayrollProviders()
        {
            var payrollProviders = await repository.GetAll();
            if (payrollProviders == null)
            {
                return NotFound();
            }
            return Ok(payrollProviders);
        }

        // GET: api/PayrollProviders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PayrollProvider>> GetPayrollProvider(int id)
        {
            var payrollProvider = await repository.GetById(id);
            if (payrollProvider == null)
            {
                return NotFound();
            }
            return Ok(payrollProvider);
        }

        // PUT: api/PayrollProvider/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPayrollProvider(int id, PayrollProvider payrollProvider)
        {
            if (id != payrollProvider.Id)
            {
                return BadRequest();
            }

            var updatedPayrollProvider = await repository.Update(payrollProvider, id);
            if (updatedPayrollProvider == null)
            {
                return NotFound();
            }

            return Ok(updatedPayrollProvider);
        }

        // POST: api/PayrollProviders

        [HttpPost]
        public async Task<ActionResult<InformationProvider>> PostInformationProvider(PayrollProvider payrollProvider)
        {
            var addedPayrollProvider = await repository.Add(payrollProvider);
            if (addedPayrollProvider == null)
            {
                return NotFound();
            }
            return Ok(addedPayrollProvider);
        }

        // DELETE: api/PayrollProviders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayrollProvider(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }
    }
}
