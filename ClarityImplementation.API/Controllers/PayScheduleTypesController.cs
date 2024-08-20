using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models.Plans;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayScheduleTypesController : Controller
    {
        private readonly GenericRepository<PayScheduleType> repository;
        private readonly GenericRepository<EmployeeBenefitsPlan> ebpRepo;
        public PayScheduleTypesController(ClarityDbContext context)
        {
            repository = new GenericRepository<PayScheduleType>(context);
            ebpRepo = new GenericRepository<EmployeeBenefitsPlan>(context);
        }

        // GET: api/PayScheduleTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PayScheduleType>>> GetPayScheduleTypes()
        {
            var payScheduleTypes = await repository.GetAll();
            if (payScheduleTypes == null)
            {
                return NotFound();
            }
            return Ok(payScheduleTypes);
        }

        // GET: api/PayScheduleTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PayScheduleType>> GetPayScheduleType(int id)
        {
            var payScheduleType = await repository.GetById(id);
            if (payScheduleType == null)
            {
                return NotFound();
            }

            return Ok(payScheduleType);
        }

        [HttpGet("ByEmployeeBenefitsPlanId/{id}")]
        public async Task<ActionResult<PayScheduleType>> GetPayScheduleTypes(int id)
        {
            var payScheduleTypes = await repository.GetAllByCompanyId(entity => entity.EmployeeBenefitsPlanId == id);
            if (payScheduleTypes == null)
            {
                return NotFound();
            }
            return Ok(payScheduleTypes);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPayScheduleType(int id, PayScheduleType payScheduleType)
        {
            if (id != payScheduleType.Id)
            {
                return BadRequest();
            }

            var updatedPayScheduleType = await repository.Update(payScheduleType, id);
            if (updatedPayScheduleType == null)
            {
                return NotFound();
            }
            return Ok(updatedPayScheduleType);
        }

        [HttpPost]
        public async Task<ActionResult<PayScheduleType>> PosPayScheduleType(PayScheduleType payScheduleType)
        {
            var addedPayScheduleType = await repository.Add(payScheduleType);
            if (addedPayScheduleType == null)
            {
                return NotFound();
            }
            var existingEBP = await ebpRepo.GetById(payScheduleType.EmployeeBenefitsPlanId);
            await ebpRepo.Update(existingEBP, payScheduleType.EmployeeBenefitsPlanId);
            return Ok(addedPayScheduleType);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayScheduleType(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }

    }
}
