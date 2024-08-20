using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models.Plans;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayrollVendorNamesController : Controller
    {
        private readonly GenericRepository<PayrollVendorName> repository;

        public PayrollVendorNamesController(ClarityDbContext context)
        {
            repository = new GenericRepository<PayrollVendorName>(context);
        }

        // GET: api/PayrollVendorNames
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PayrollVendorName>>> GetPayrollVendorNames()
        {
            var payrollVendorNames = await repository.GetAll();
            if (payrollVendorNames == null)
            {
                return NotFound();
            }
            return Ok(payrollVendorNames);
        }

        // GET: api/PayrollVendorName/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PayrollVendorName>> GetPayrollVendorName(int id)
        {
            var payrollVendorName = await repository.GetById(id);
            if (payrollVendorName == null)
            {
                return NotFound();
            }
            return Ok(payrollVendorName);
        }

        // PUT: api/PayrollVendorName/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPayrollVendorName(int id, PayrollVendorName payrollVendorName)
        {
            if (id != payrollVendorName.Id)
            {
                return BadRequest();
            }

            var updatedPayrollVendorName = await repository.Update(payrollVendorName, id);
            if (updatedPayrollVendorName == null)
            {
                return NotFound();
            }

            return Ok(updatedPayrollVendorName);
        }

        // POST: api/PayrollVendorNames

        [HttpPost]
        public async Task<ActionResult<PayrollVendorName>> PostPayrollVendorName(PayrollVendorName payrollVendorName)
        {
            var addedPayrollVendorName = await repository.Add(payrollVendorName);
            if (addedPayrollVendorName == null)
            {
                return NotFound();
            }
            return Ok(addedPayrollVendorName);
        }

        // DELETE: api/PayrollVendorName/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayrollVendorName(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }
    }
}
