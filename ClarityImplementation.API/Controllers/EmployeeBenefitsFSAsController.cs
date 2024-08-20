using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models.Plans;
using ClarityImplementation.API.Models;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Configuration;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeBenefitsFSAsController : ControllerBase
    {
        private readonly GenericRepository<EmployeeBenefitsFSA> repository;
        private readonly GenericRepository<EmployeeBenefitsPlan> planRepo;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public EmployeeBenefitsFSAsController(ClarityDbContext context, IConfiguration configuration)
        {
            repository = new GenericRepository<EmployeeBenefitsFSA>(context);
            planRepo = new GenericRepository<EmployeeBenefitsPlan>(context);
            _httpClient = new HttpClient();
            _configuration = configuration;
        }

        // GET: api/EmployeeBenefitsFSAs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeBenefitsFSA>>> GetEmployeeBenefitsFSAs()
        {
            var employeeBenefitsFSAs = await repository.GetAll();
            if (employeeBenefitsFSAs == null)
            {
                return NotFound();
            }
            return Ok(employeeBenefitsFSAs);
        }

        [HttpGet("ByEmployeeBenefitsPlanId/{id}")]
        public async Task<ActionResult<EmployeeBenefitsFSA>> GetEmployeeBenefitsFSAByPlanId(int id)
        {
            var baseURL = _configuration.GetValue<string>("BaseURL");

            var employeeBenefitsFSA = await repository.GetByCompanyId(entity => entity.EmployeeBenefitsPlanId == id);
            if (employeeBenefitsFSA == null)
            {
                return NotFound();
            }

            var response = await _httpClient.GetAsync(baseURL + "/EmployeeBenefitsDCAs/ByEmployeeBenefitsFSAId/" + employeeBenefitsFSA.Id);
            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                var employeeBenefitsDCA = JsonConvert.DeserializeObject<EmployeeBenefitsDCA>(apiResponse);
                if (employeeBenefitsDCA != null)
                {
                    employeeBenefitsFSA.EmployeeBenefitsDCA = employeeBenefitsDCA;
                }
            }
            var lpfsaResponse = await _httpClient.GetAsync(baseURL + "/EmployeeBenefitsLPFSAs/ByEmployeeBenefitsFSAId/" + employeeBenefitsFSA.Id);
            if (lpfsaResponse.IsSuccessStatusCode)
            {
                string apiResponse = await lpfsaResponse.Content.ReadAsStringAsync();
                var employeeBenefitsLPFSA = JsonConvert.DeserializeObject<EmployeeBenefitsLPFSA>(apiResponse);
                if (employeeBenefitsLPFSA != null)
                {
                    employeeBenefitsFSA.EmployeeBenefitsLPFSA = employeeBenefitsLPFSA;
                }
            } 

            return Ok(employeeBenefitsFSA);
        }

        [HttpGet("{id}")]
        //[HttpGet("{id}")]
        public async Task<ActionResult<EmployeeBenefitsFSA>> GetEmployeeBenefitsFSA(int id)
        {
            var baseURL = _configuration.GetValue<string>("BaseURL");
            var employeeBenefitsFSA = await repository.GetById(id);
            if (employeeBenefitsFSA == null)
            {
                return NotFound();
            }
            var response = await _httpClient.GetAsync(baseURL + "/EmployeeBenefitsDCAs/ByEmployeeBenefitsFSAId/" + employeeBenefitsFSA.Id);
            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                var employeeBenefitsDCA = JsonConvert.DeserializeObject<EmployeeBenefitsDCA>(apiResponse);
                if (employeeBenefitsDCA !=  null)
                {
                    employeeBenefitsFSA.EmployeeBenefitsDCA = employeeBenefitsDCA;
                }
                
            }

            var response2 = await _httpClient.GetAsync(baseURL + "/EmployeeBenefitsLPFSAs/ByEmployeeBenefitsFSAId/" + employeeBenefitsFSA.Id);
            if (response2.IsSuccessStatusCode)
            {
                string apiResponse = await response2.Content.ReadAsStringAsync();
                var employeeBenefitsLPFSA = JsonConvert.DeserializeObject<EmployeeBenefitsLPFSA>(apiResponse);
                if (employeeBenefitsLPFSA != null)
                {
                    employeeBenefitsFSA.EmployeeBenefitsLPFSA = employeeBenefitsLPFSA;
                }
            }

            return Ok(employeeBenefitsFSA);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeBenefitsFSA(int id, EmployeeBenefitsFSA employeeBenefitsFSA)
        {
            if (id != employeeBenefitsFSA.Id)
            {
                return BadRequest();
            }

            var updatedEmployeeBenefitsFSA = await repository.Update(employeeBenefitsFSA, id);
            if (updatedEmployeeBenefitsFSA == null)
            {
                return NotFound();
            }
            return Ok(updatedEmployeeBenefitsFSA);
        }

        [HttpPost]
        public async Task<ActionResult<IActionResult>> PostEmployeeBenefitsFSA(EmployeeBenefitsFSA employeeBenefitsFSA)
        {

            var addedEmployeeBenefitsFSA = await repository.Add(employeeBenefitsFSA);
            if (addedEmployeeBenefitsFSA == null)
            {
                return NotFound();
            }
            var existingEmployeeBenefitsPlan = await planRepo.GetById(employeeBenefitsFSA.EmployeeBenefitsPlanId);
            await planRepo.Update(existingEmployeeBenefitsPlan, employeeBenefitsFSA.EmployeeBenefitsPlanId);
            return Ok(addedEmployeeBenefitsFSA);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeBenefitsFSA(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }
    }
}
