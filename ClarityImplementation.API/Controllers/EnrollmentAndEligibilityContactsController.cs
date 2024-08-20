using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models.Plans;
using ClarityImplementation.API.Models;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using ClarityImplementation.API.Models.Plans.COBRA;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentAndEligibilityContactsController : Controller
    {
        private readonly GenericRepository<EnrollmentAndEligibilityContact> repository;
        private readonly GenericRepository<COBRABrokerContact> cobraBrokerContactRepository;
        private readonly GenericRepository<COBRAClientContact> cobraClientContactRepository;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public EnrollmentAndEligibilityContactsController(ClarityDbContext context, IConfiguration configuration)
        {
            repository = new GenericRepository<EnrollmentAndEligibilityContact>(context);
            cobraBrokerContactRepository = new GenericRepository<COBRABrokerContact>(context);
            cobraClientContactRepository = new GenericRepository<COBRAClientContact>(context);
            _httpClient = new HttpClient();
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnrollmentAndEligibilityContact>>> GetEnrollmentAndEligibilityContacts()
        {
            var enrollmentAndEligibilityContacts = await repository.GetAll();
            if (enrollmentAndEligibilityContacts == null)
            {
                return NotFound();
            }
            return Ok(enrollmentAndEligibilityContacts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EnrollmentAndEligibilityContact>> GetEnrollmentAndEligibilityContact(int id)
        {
            var baseURL = _configuration.GetValue<string>("BaseURL");

            var enrollmentAndEligibilityContact = await repository.GetById(id);
            if (enrollmentAndEligibilityContact == null)
            {
                return NotFound();
            }

            return Ok(enrollmentAndEligibilityContact);
        }


        [HttpGet("ByPlanId/{id}")]
        public async Task<ActionResult<List<EnrollmentAndEligibilityContact>>> GetEnrollmentAndEligibilityContactByCobraId(int id)
        {
            var baseURL = _configuration.GetValue<string>("BaseURL");

            // Fetch all contacts related to the COBRAPlanId
            var enrollmentAndEligibilityContacts = await repository.GetAllByCompanyId(entity => entity.COBRAPlanId == id);
            if (enrollmentAndEligibilityContacts == null )
            {
                return NotFound();
            }

            return Ok(enrollmentAndEligibilityContacts);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEnrollmentAndEligibilityContact(int id, EnrollmentAndEligibilityContact enrollmentAndEligibilityContact)
        {
            if (id != enrollmentAndEligibilityContact.Id)
            {
                return BadRequest();
            }

            var updatedEnrollmentAndEligibilityContact = await repository.Update(enrollmentAndEligibilityContact, id);
            if (updatedEnrollmentAndEligibilityContact == null)
            {
                return NotFound();
            }
            return Ok(updatedEnrollmentAndEligibilityContact);
        }

        [HttpPost]
        public async Task<ActionResult<EnrollmentAndEligibilityContact>> PostEnrollmentAndEligibilityContact([FromBody] EnrollmentAndEligibilityContact enrollmentAndEligibilityContact)
        {
            if (enrollmentAndEligibilityContact == null)
            {
                return BadRequest("Contact data is null.");
            }

            

            // Add the new contact to the repository
            var addedEnrollmentAndEligibilityContact = await repository.Add(enrollmentAndEligibilityContact);

            // If adding fails for some reason
            if (addedEnrollmentAndEligibilityContact == null)
            {
                return StatusCode(500, "An error occurred while creating the contact.");
            }

            // Return a 201 Created response with the location of the newly created resource
            return CreatedAtAction(nameof(GetEnrollmentAndEligibilityContactByCobraId),
                                    new { id = addedEnrollmentAndEligibilityContact.COBRAPlanId },
                                    addedEnrollmentAndEligibilityContact);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnrollmentAndEligibilityContact(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }
    }
}
