using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models.Plans;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayScheduleFrequenciesController : Controller
    {

        private readonly GenericRepository<PayScheduleFrequency> repository;

        public PayScheduleFrequenciesController(ClarityDbContext context)
        {
            repository = new GenericRepository<PayScheduleFrequency>(context);
        }

        // GET: api/PayScheduleFrequencies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PayScheduleFrequency>>> GetPayScheduleFrequencies()
        {
            var payScheduleFrequencies = await repository.GetAll();
            if (payScheduleFrequencies == null)
            {
                return NotFound();
            }
            return Ok(payScheduleFrequencies);
        }

        // GET: api/PayScheduleFrequencies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PayScheduleFrequency>> GetPayScheduleFrequency(int id)
        {
            var payScheduleFrequency = await repository.GetById(id);
            if (payScheduleFrequency == null)
            {
                return NotFound();
            }
            return Ok(payScheduleFrequency);
        }

        // PUT: api/PayScheduleFrequency/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPayScheduleFrequency(int id, PayScheduleFrequency payScheduleFrequency)
        {
            if (id != payScheduleFrequency.Id)
            {
                return BadRequest();
            }

            var updatedPayScheduleFrequency = await repository.Update(payScheduleFrequency, id);
            if (updatedPayScheduleFrequency == null)
            {
                return NotFound();
            }

            return Ok(updatedPayScheduleFrequency);
        }

        // POST: api/PayScheduleFrequencies

        [HttpPost]
        public async Task<ActionResult<PayScheduleFrequency>> PostInformationProvider(PayScheduleFrequency payScheduleFrequency)
        {
            var addedPayScheduleFrequency = await repository.Add(payScheduleFrequency);
            if (addedPayScheduleFrequency == null)
            {
                return NotFound();
            }
            return Ok(addedPayScheduleFrequency);
        }

        // DELETE: api/PayScheduleFrequencies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayScheduleFrequency(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }
    }
}
