using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiCaseContactChangeRequestsController : ControllerBase
    {
        private readonly GenericRepository<ApiCaseContactChangeRequest> repository;
        private readonly GenericRepository<Company> companyRepository;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public ApiCaseContactChangeRequestsController(ClarityDbContext context, IConfiguration configuration)
        {
            repository = new GenericRepository<ApiCaseContactChangeRequest>(context);
            companyRepository = new GenericRepository<Company>(context);
            _httpClient = new HttpClient();
            _configuration = configuration;
        }
        [HttpPost]
        public async Task<ActionResult<ApiCaseContactChangeRequest>> PostAddress(ApiCaseContactChangeRequest apiCaseContactChangeRequest)
        {
            
            apiCaseContactChangeRequest.ContactIsBenAdminContact = false;
            apiCaseContactChangeRequest.ContactIsCobraContact = false;
            apiCaseContactChangeRequest.ContactIsConsBenContact = false;
            apiCaseContactChangeRequest.CreatedAt = DateTime.UtcNow;
            apiCaseContactChangeRequest.UpdatedAt = DateTime.UtcNow;
            //apiCaseContactChangeRequest.OrganizationName =;
            //apiCaseContactChangeRequest.EmployerName =;
            //apiCaseContactChangeRequest.ContactType =;
            //apiCaseContactChangeRequest.ContactPhone =;
            //apiCaseContactChangeRequest.ContactLastName =;
            //apiCaseContactChangeRequest.ContactFirstName =;
            //apiCaseContactChangeRequest.ContactEmail =;
            //apiCaseContactChangeRequest.Status =;
            //apiCaseContactChangeRequest.CaseType =;
            //apiCaseContactChangeRequest.CaseSubType =;

            var addedApiCaseContactChangeRequest = await repository.Add(apiCaseContactChangeRequest);
            if (addedApiCaseContactChangeRequest == null)
            {
                return NotFound();
            }
            return Ok(addedApiCaseContactChangeRequest);
        }
    }
}
