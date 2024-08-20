using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models;
using ClarityImplementation.API.Models.Funding;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Newtonsoft.Json;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiCaseContactsController : Controller
    {
        private readonly GenericRepository<Api_Case_Contact> repository;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ApiCaseContactsController(ClarityDbContext context, IConfiguration configuration)
        {
            repository = new GenericRepository<Api_Case_Contact>(context);
            _httpClient = new HttpClient();
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<ActionResult<Api_Case_Contact>> PostContact(Api_Case_Contact apiCaseContact)
        {
            apiCaseContact.CaseContactId = Guid.NewGuid().ToString();
            apiCaseContact.UpdatedAt = DateTime.UtcNow;
            apiCaseContact.CreatedAt = DateTime.UtcNow;
            var addedApiCaseContact = await repository.Add(apiCaseContact);
            if (addedApiCaseContact == null)
            {
                return NotFound();
            }
            return Ok(addedApiCaseContact);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Api_Case_Contact>>> GetAllContacts()
        {

            var contacts = await repository.GetAll();
            if (contacts == null)
            {
                return NotFound();
            }
            
            return Ok(contacts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Api_Case_Contact>> GetContact(string id)
        {
            var contact = await repository.GetByCaseId(id);
            if (contact == null)
            {
                return NotFound();
            }

            //if (contact.Role != null && contact.Role != "")
            //{
            //    string[] selectedRoles = contact.Role.Split(','); 
            //    contact.SelectedRoles = selectedRoles.ToList(); 
            //}
            //if (contact.Responsibility != null && contact.Responsibility != "")
            //{
            //    string[] selectedResponsibilities = contact.Responsibility.Split(','); 
            //    contact.SelectedResponsibilities = selectedResponsibilities.ToList(); 
            //}
            return Ok(contact);
        }

        [HttpGet("ByCompanyId/{id}")]
        public async Task<ActionResult<IEnumerable<Api_Case_Contact>>> GetAllContactByCompany(int id)
        {
            var contacts = await repository.GetAllByCompanyId(entity => entity.CompanyId == id);
            if (contacts == null)
            {
                return NotFound();
            }
            foreach (var contact in contacts)
            {
                //if (contact.Role != null && contact.Role != "")
                //{
                //    string[] selectedRoles = contact.Role.Split(','); 
                //    contact.SelectedRoles = selectedRoles.ToList(); 
                //}
                //if (contact.Responsibility != null && contact.Responsibility != "")
                //{
                //    string[] selectedResponsibilities = contact.Responsibility.Split(','); 
                //    contact.SelectedResponsibilities = selectedResponsibilities.ToList(); 
                //}
            }
            
            return Ok(contacts);
        }

        [HttpGet("ByCaseId/{id}")]
        public async Task<ActionResult<IEnumerable<Api_Case_Contact>>> GetAllContactByCompany(String id)
        {
            var contacts = await repository.GetAllByCompanyId(entity => entity.CaseId == id);
            if (contacts == null)
            {
                return NotFound();
            }

            return Ok(contacts);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact(string id, Api_Case_Contact apiCaseContact)
        {
            if (id != apiCaseContact.CaseContactId)
            {
                return BadRequest();
            }

            var updatedContact = await repository.UpdateByString(apiCaseContact, id);
            if (updatedContact == null)
            {
                return NotFound();
            }
            return Ok(updatedContact);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(string id)
        {
            var response = await repository.DeleteByString(id);
            return Ok(response);
        }
    }
}
