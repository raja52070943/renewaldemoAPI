using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models.Plans;
using ClarityImplementation.API.Models.Plans.COBRA;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class COBRABrokerContactsController : Controller
    {
        private readonly GenericRepository<COBRABrokerContact> repository;
        private readonly GenericRepository<EnrollmentAndEligibilityContact> enrollmentAndEligibilityContactRepository;
        public COBRABrokerContactsController(ClarityDbContext context)
        {
            repository = new GenericRepository<COBRABrokerContact>(context);
            enrollmentAndEligibilityContactRepository = new GenericRepository<EnrollmentAndEligibilityContact>(context);
        }

        // GET: api/MidYearPlans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<COBRABrokerContact>>> GetCOBRABrokerContacts()
        {
            var cobraBrokerContacts = await repository.GetAll();
            if (cobraBrokerContacts == null)
            {
                return NotFound();
            }
            return Ok(cobraBrokerContacts);
        }

        // GET: api/MidYearPlans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<COBRABrokerContact>> GetCOBRABrokerContact(int id)
        {
            var cobraBrokerContact = await repository.GetById(id);
            if (cobraBrokerContact == null)
            {
                return NotFound();
            }

            return Ok(cobraBrokerContact);
        }

        //[HttpGet("{id}")]
        [HttpGet("ByEnrollmentAndEligibilityContactId/{id}")]
        public async Task<ActionResult<COBRABrokerContact>> GetCOBRABrokerContactByContactId(int id)
        {
            var cobraBrokerContacts = await repository.GetAllByCompanyId(entity => entity.EnrollmentAndEligibilityContactId == id);
            if (cobraBrokerContacts == null)
            {
                return NotFound();
            }
            return Ok(cobraBrokerContacts);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCOBRABrokerContact(int id, COBRABrokerContact cobraBrokerContact)
        {
            if (id != cobraBrokerContact.Id)
            {
                return BadRequest();
            }

            var updatedCOBRABrokerContact = await repository.Update(cobraBrokerContact, id);
            if (updatedCOBRABrokerContact == null)
            {
                return NotFound();
            }
            return Ok(updatedCOBRABrokerContact);
        }

        [HttpPost]
        public async Task<ActionResult<COBRABrokerContact>> PostCOBRABrokerContact(COBRABrokerContact cobraBrokerContact)
        {
            var addedCOBRABrokerContact = await repository.Add(cobraBrokerContact);
            if (addedCOBRABrokerContact == null)
            {
                return NotFound();
            }
            var existingEnrolllmentAndEligibilityContact = await enrollmentAndEligibilityContactRepository.GetById(cobraBrokerContact.EnrollmentAndEligibilityContactId);
            await enrollmentAndEligibilityContactRepository.Update(existingEnrolllmentAndEligibilityContact, cobraBrokerContact.EnrollmentAndEligibilityContactId);
            return Ok(addedCOBRABrokerContact);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCOBRABrokerContact(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }

    }
}
