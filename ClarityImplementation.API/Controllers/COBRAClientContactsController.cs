using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models.Plans.COBRA;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class COBRAClientContactsController : Controller
    {
        private readonly GenericRepository<COBRAClientContact> repository;
        private readonly GenericRepository<EnrollmentAndEligibilityContact> enrollmentAndEligibilityContactRepository;
        public COBRAClientContactsController(ClarityDbContext context)
        {
            repository = new GenericRepository<COBRAClientContact>(context);
            enrollmentAndEligibilityContactRepository = new GenericRepository<EnrollmentAndEligibilityContact>(context);
        }

        // GET: api/MidYearPlans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<COBRAClientContact>>> GetCOBRAClientContacts()
        {
            var cobraClientContacts = await repository.GetAll();
            if (cobraClientContacts == null)
            {
                return NotFound();
            }
            return Ok(cobraClientContacts);
        }

        // GET: api/MidYearPlans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<COBRAClientContact>> GetCOBRAClientContact(int id)
        {
            var cobraClientContact = await repository.GetById(id);
            if (cobraClientContact == null)
            {
                return NotFound();
            }

            return Ok(cobraClientContact);
        }

        //[HttpGet("{id}")]
        [HttpGet("ByEnrollmentAndEligibilityContactId/{id}")]
        public async Task<ActionResult<COBRAClientContact>> GetCOBRAClientContactByContactId(int id)
        {
            var cobraClientContacts = await repository.GetAllByCompanyId(entity => entity.EnrollmentAndEligibilityContactId == id);
            if (cobraClientContacts == null)
            {
                return NotFound();
            }
            return Ok(cobraClientContacts);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCOBRAClientContact(int id, COBRAClientContact cobraClientContact)
        {
            if (id != cobraClientContact.Id)
            {
                return BadRequest();
            }

            var updatedCOBRAClientContact = await repository.Update(cobraClientContact, id);
            if (updatedCOBRAClientContact == null)
            {
                return NotFound();
            }
            return Ok(updatedCOBRAClientContact);
        }

        [HttpPost]
        public async Task<ActionResult<COBRAClientContact>> PostCOBRAClientContact(COBRAClientContact cobraClientContact)
        {
            var addedCOBRAClientContact = await repository.Add(cobraClientContact);
            if (addedCOBRAClientContact == null)
            {
                return NotFound();
            }
            var existingEnrolllmentAndEligibilityContact = await enrollmentAndEligibilityContactRepository.GetById(cobraClientContact.EnrollmentAndEligibilityContactId);
            await enrollmentAndEligibilityContactRepository.Update(existingEnrolllmentAndEligibilityContact, cobraClientContact.EnrollmentAndEligibilityContactId);
            return Ok(addedCOBRAClientContact);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCOBRAClientContact(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }
    }
}
