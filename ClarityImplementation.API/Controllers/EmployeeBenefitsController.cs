using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models.FileUpload;
using ClarityImplementation.API.Models.Funding;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Configuration;
using System.Net.Http;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeBenefitsController : Controller
    {
        private readonly GenericRepository<EmployeeBenefit> repository;

        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly GenericRepository<EmployeeBenefitsFileUploadItem> employeeBenefitsFileUploadItemRepository;

        public EmployeeBenefitsController(ClarityDbContext context, IConfiguration configuration)
        {
            repository = new GenericRepository<EmployeeBenefit>(context);
            _httpClient = new HttpClient();
            _configuration = configuration;
            employeeBenefitsFileUploadItemRepository = new GenericRepository<EmployeeBenefitsFileUploadItem>(context);
        }

        // GET: api/EmployeeBenefits
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeBenefit>>> GetEmployeeBenefits()
        {
            var employeeBenefits = await repository.GetAll();
            if (employeeBenefits == null)
            {
                return NotFound();
            }
            return Ok(employeeBenefits);
        }

        // GETById: api/EmployeeBenefits/id
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeBenefit>> GetEmployeeBenefit(int id)
        {
            var employeeBenefit = await repository.GetById(id);
            if (employeeBenefit == null)
            {
                return NotFound();
            }
            var baseURL = _configuration.GetValue<string>("BaseURL");

            var response = await _httpClient.GetAsync(baseURL + "/EmployeeBenefitsFileUploadItems/ByEmployeeBenefitsFileUploadId/" + employeeBenefit.Id);
            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                var employeeBenefitsFileUploadItems = JsonConvert.DeserializeObject<List<EmployeeBenefitsFileUploadItem>>(apiResponse);
                if (employeeBenefitsFileUploadItems != null)
                {
                    employeeBenefit.EmployeeBenefitsFileUploadItems = employeeBenefitsFileUploadItems;
                }
                else
                {
                    employeeBenefit.EmployeeBenefitsFileUploadItems = new List<EmployeeBenefitsFileUploadItem>();
                }
            }
            return Ok(employeeBenefit);

        }

        // GET: api/EmployeeBenefits/ByFileId/5
        [HttpGet("ByFileId/{id}")]
        public async Task<ActionResult<IEnumerable<EmployeeBenefit>>> GetEmployeeBenefits(int id)
        {
            var employeeBenefits = await repository.GetAllByCompanyId(entity => entity.FileId == id);
            if (employeeBenefits == null)
            {
                return NotFound();
            }
            var baseURL = _configuration.GetValue<string>("BaseURL");
            foreach(var employeeBenefit in employeeBenefits)
            {
                var response = await _httpClient.GetAsync(baseURL + "/EmployeeBenefitsFileUploadItems/ByEmployeeBenefitsFileUploadId/" + employeeBenefit.Id);
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var employeeBenefitsFileUploadItems = JsonConvert.DeserializeObject<List<EmployeeBenefitsFileUploadItem>>(apiResponse);
                    if (employeeBenefitsFileUploadItems != null)
                    {
                        employeeBenefit.EmployeeBenefitsFileUploadItems = employeeBenefitsFileUploadItems;
                    }
                    else
                    {
                        employeeBenefit.EmployeeBenefitsFileUploadItems = new List<EmployeeBenefitsFileUploadItem>();
                    }

                }
            }
            
            return Ok(employeeBenefits);
        }

        // PUT: api/EmployeeBenefits/id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeBenefit(int id, EmployeeBenefit employeeBenefit)
        {
            if (id != employeeBenefit.Id)
            {
                return BadRequest();
            }

            var updatedEmployeeBenefit = await repository.Update(employeeBenefit, id);
            if (updatedEmployeeBenefit == null)
            {
                return NotFound();
            }
            return Ok(updatedEmployeeBenefit);
        }

        // POST: api/EmployeeBenefits
        [HttpPost]
        public async Task<ActionResult<EmployeeBenefit>> PostEmployeeBenefit(EmployeeBenefit employeeBenefit)
        {

            var addedEmployeeBenefit = await repository.Add(employeeBenefit);
            if (addedEmployeeBenefit == null)
            {
                return NotFound();
            }

            EmployeeBenefitsFileUploadItem employeeBenefitsFileUploadItem = new EmployeeBenefitsFileUploadItem();
            employeeBenefitsFileUploadItem.EmployeeBenefitId = addedEmployeeBenefit.Id;
            await employeeBenefitsFileUploadItemRepository.Add(employeeBenefitsFileUploadItem);

            return RedirectToAction("GetEmployeeBenefit", new { id = addedEmployeeBenefit.Id });
        }

        // DELETE: api/EmployeeBenefits/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeBenefit(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }
    }
}
