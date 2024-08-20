using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models;
using ClarityImplementation.API.Models.Communication;
using ClarityImplementation.API.Models.FileUpload;
using ClarityImplementation.API.Models.Plans.COBRA;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeBenefitsFileUploadsController : Controller
    {
        private readonly GenericRepository<EmployeeBenefitsFileUpload> repository;
        private readonly GenericRepository<EmployeeBenefit> employeeBenefitRepository;

        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public EmployeeBenefitsFileUploadsController(ClarityDbContext context, IConfiguration configuration)
        {
            repository = new GenericRepository<EmployeeBenefitsFileUpload>(context);
            _httpClient = new HttpClient();
            _configuration = configuration;
        }

        // GET: api/EmployeeBenefitsFileUploads
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeBenefitsFileUpload>>> GetEmployeeBenefitsFileUploads()
        {
            var employeeBenefitsFileUploads = await repository.GetAll();
            if (employeeBenefitsFileUploads == null)
            {
                return NotFound();
            }
            return Ok(employeeBenefitsFileUploads);
        }

        // GETById: api/EmployeeBenefitsFileUploads/id
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeBenefitsFileUpload>> GetEmployeeBenefitsFileUpload(int id)
        {
            var baseURL = _configuration.GetValue<string>("BaseURL");

            var employeeBenefitsFileUpload = await repository.GetById(id);
            if (employeeBenefitsFileUpload == null)
            {
                return NotFound();
            }
            var response = await _httpClient.GetAsync(baseURL + "/CobraBenefits/ByFileId/" + id);
            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                var cobraBenefits = JsonConvert.DeserializeObject<List<CobraBenefit>>(apiResponse);
                employeeBenefitsFileUpload.CobraBenefits = cobraBenefits;
            }

            var response2 = await _httpClient.GetAsync(baseURL + "/EmployeeBenefits/ByFileId/" + id);
            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response2.Content.ReadAsStringAsync();
                var employeeBenefits = JsonConvert.DeserializeObject<List<EmployeeBenefit>>(apiResponse);
                employeeBenefitsFileUpload.EmployeeBenefits = employeeBenefits;
            }

            return Ok(employeeBenefitsFileUpload);
        }

        // GET: api/CompanyAddresses/ByCompanyId/5
        [HttpGet("ByCompanyId/{id}")]
        public async Task<ActionResult<EmployeeBenefitsFileUpload>> GetEmployeeBenefitsFileUploads(int id)
        {
            var employeeBenefitsFileUploads = await repository.GetByCompanyId(entity => entity.CompanyId == id);
            if (employeeBenefitsFileUploads == null)
            {
                return NotFound();
            }
            return Ok(employeeBenefitsFileUploads);
        }

        // PUT: api/EmployeeBenefitsFileUploads/id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeBenefitsFileUpload(int id, EmployeeBenefitsFileUpload employeeBenefitsFileUpload)
        {
            if (id != employeeBenefitsFileUpload.Id)
            {
                return BadRequest();
            }

            var updatedEmployeeBenefitsFileUpload = await repository.Update(employeeBenefitsFileUpload, id);
            if (updatedEmployeeBenefitsFileUpload == null)
            {
                return NotFound();
            }
            return Ok(updatedEmployeeBenefitsFileUpload);
        }

        // POST: api/EmployeeBenefitsFileUploads
        [HttpPost]
        public async Task<ActionResult<EmployeeBenefitsFileUpload>> PostEmployeeBenefitsFileUpload(EmployeeBenefitsFileUpload employeeBenefitsFileUpload)
        {

            var addedEmployeeBenefitsFileUpload = await repository.Add(employeeBenefitsFileUpload);
            if (addedEmployeeBenefitsFileUpload == null)
            {
                return NotFound();
            }
            string[] fileCategories = {
                "Summary of Benefits and Coverage (SBC) Documents",
                "Medical Carrier Invoice",
                "Age/Gender Banded Rates",
                "Age/Gender Banded Rates"
            };

            List<CobraBenefit> cobraBenefitsList = new List<CobraBenefit>();
            for (int i = 0; i < 4; i++)
            {
                CobraBenefit cobraBenefit = new CobraBenefit
                {
                    FileCategory = fileCategories[i],
                };
                cobraBenefitsList.Add(cobraBenefit);
            }

            addedEmployeeBenefitsFileUpload.CobraBenefits = cobraBenefitsList;

            string[] benefitsFileCategories = {
                    "Employee Benefits File Upload"
                };

            List<EmployeeBenefit> employeeBenefitsList = new List<EmployeeBenefit>();
            for (int i = 0; i < 4; i++)
            {
                EmployeeBenefit employeeBenefit = new EmployeeBenefit
                {
                    FileCategory = benefitsFileCategories[i],
                };
                employeeBenefitsList.Add(employeeBenefit);
            }

            addedEmployeeBenefitsFileUpload.EmployeeBenefits = employeeBenefitsList;

            var updatedEmployeeBenefitsFileUpload = await repository.Update(addedEmployeeBenefitsFileUpload, addedEmployeeBenefitsFileUpload.Id);
            if (updatedEmployeeBenefitsFileUpload == null)
            {
                return NotFound();
            }

            return Ok(updatedEmployeeBenefitsFileUpload);

            //return Ok(addedEmployeeBenefitsFileUpload);
        }

        // DELETE: api/EmployeeBenefitsFileUploads/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeBenefitsFileUpload(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }
    }
}
