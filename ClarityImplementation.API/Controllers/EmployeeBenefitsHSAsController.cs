using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models.Plans;
using ClarityImplementation.API.Models;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeBenefitsHSAsController : ControllerBase
    {
        private readonly GenericRepository<EmployeeBenefitsHSA> repository;
        private readonly GenericRepository<EmployeeContributionGroup> ecgRepo;
        private readonly GenericRepository<EmployeeBenefitsPlan> planRepo;

        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public EmployeeBenefitsHSAsController(ClarityDbContext context, IConfiguration configuration)
        {
            repository = new GenericRepository<EmployeeBenefitsHSA>(context);
            planRepo = new GenericRepository<EmployeeBenefitsPlan>(context);
            ecgRepo = new GenericRepository<EmployeeContributionGroup>(context);
            _httpClient = new HttpClient();
            _configuration = configuration;
        }

        // GET: api/EmployeeBenefitsHSAs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeBenefitsHSA>>> GetEmployeeBenefitsHSAs()
        {
            var employeeBenefitsHSAs = await repository.GetAll();
            if (employeeBenefitsHSAs == null)
            {
                return NotFound();
            }
            return Ok(employeeBenefitsHSAs);
        }

        [HttpGet("ByEmployeeBenefitsPlanId/{id}")]
        public async Task<ActionResult<EmployeeBenefitsHSA>> GetEmployeeBenefitsHSAByPlanId(int id)
        {
            var baseURL = _configuration.GetValue<string>("BaseURL");

            var employeeBenefitsHSA = await repository.GetByCompanyId(entity => entity.EmployeeBenefitsPlanId == id);
            if (employeeBenefitsHSA == null)
            {
                return NotFound();
            }
            var response = await _httpClient.GetAsync(baseURL + "/EmployeeContributionGroups/ByEmployeeBenefitsHSAId/" + employeeBenefitsHSA.Id);
            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                var employeeContributionGroup = JsonConvert.DeserializeObject<List<EmployeeContributionGroup>>(apiResponse);
                employeeBenefitsHSA.EmployeeContributionGroups = employeeContributionGroup;
            }
            return Ok(employeeBenefitsHSA);
        }

        [HttpGet("{id}")]
        //[HttpGet("{id}")]
        public async Task<ActionResult<EmployeeBenefitsHSA>> GetEmployeeBenefitsHSA(int id)
        {
            var baseURL = _configuration.GetValue<string>("BaseURL");

            var employeeBenefitsHSA = await repository.GetById(id);
            if (employeeBenefitsHSA == null)
            {
                return NotFound();
            }

            if (employeeBenefitsHSA.IsEmployerContribution == "true")
            {
                var hsaResponse = await _httpClient.GetAsync(baseURL + "/EmployeeContributionGroups/ByEmployeeBenefitsHSAId/" + id);
                if (hsaResponse.IsSuccessStatusCode)
                {
                    string apiResponse = await hsaResponse.Content.ReadAsStringAsync();
                    //var address = JsonConvert.DeserializeObject<List<Address>>(apiResponse);
                    var employeeBenefitsHSAEmployeeContribution = JsonConvert.DeserializeObject<List<EmployeeContributionGroup>>(apiResponse);
                    employeeBenefitsHSA.EmployeeContributionGroups = employeeBenefitsHSAEmployeeContribution;
                }
            }
            else
            {
                employeeBenefitsHSA.EmployeeContributionGroups = new List<EmployeeContributionGroup>();
            }

            return Ok(employeeBenefitsHSA);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeBenefitsHSA(int id, EmployeeBenefitsHSA employeeBenefitsHSA)
        {
            if (id != employeeBenefitsHSA.Id)
            {
                return BadRequest();
            }

            var updatedEmployeeBenefitsHSA = await repository.Update(employeeBenefitsHSA, id);
            if (updatedEmployeeBenefitsHSA == null)
            {
                return NotFound();
            }
            return Ok(updatedEmployeeBenefitsHSA);
        }

        [HttpPost]
        public async Task<ActionResult<IActionResult>> PostEmployeeBenefitsHSA(EmployeeBenefitsHSA employeeBenefitsHSA)
        {

            var addedEmployeeBenefitsHSA = await repository.Add(employeeBenefitsHSA);
            if (addedEmployeeBenefitsHSA == null)
            {
                return NotFound();
            }
            EmployeeContributionGroup employeeContributionGroup = new EmployeeContributionGroup();
            employeeContributionGroup.EmployeeBenefitsHSAId = addedEmployeeBenefitsHSA.Id;
            await ecgRepo.Add(employeeContributionGroup);

            return RedirectToAction("GetEmployeeBenefitsHSA", new { id = addedEmployeeBenefitsHSA.Id });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeBenefitsHSA(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }
    }
}
