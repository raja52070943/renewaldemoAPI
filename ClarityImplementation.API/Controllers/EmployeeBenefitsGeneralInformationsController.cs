using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models.Plans;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeBenefitsGeneralInformationsController : Controller
    {
        private readonly GenericRepository<EmployeeBenefitsGeneralInformation> repository;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public EmployeeBenefitsGeneralInformationsController(ClarityDbContext context, IConfiguration configuration)
        {
            repository = new GenericRepository<EmployeeBenefitsGeneralInformation>(context);
            _httpClient = new HttpClient();
            _configuration = configuration;
        }

        // GET: api/MidYearPlans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeBenefitsGeneralInformation>>> GetEmployeeBenefitsGeneralInformations()
        {
            var employeeBenefitsGeneralInformations = await repository.GetAll();
            if (employeeBenefitsGeneralInformations == null)
            {
                return NotFound();
            }
            return Ok(employeeBenefitsGeneralInformations);
        }

        // GET: api/MidYearPlans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeBenefitsGeneralInformation>> GetEmployeeBenefitsGeneralInformation(int id)
        {
            var baseURL = _configuration.GetValue<string>("BaseURL");
            var employeeBenefitsGeneralInformation = await repository.GetById(id);
            if (employeeBenefitsGeneralInformation == null)
            {
                return NotFound();
            }
            return Ok(employeeBenefitsGeneralInformation);
        }

        //[HttpGet("{id}")]
        [HttpGet("ByEmployeeBenefitsPlanId/{id}")]
        public async Task<ActionResult<EmployeeBenefitsGeneralInformation>> GetEmployeeBenefitsGeneralInformationByPlan(int id)
        {
            var baseURL = _configuration.GetValue<string>("BaseURL");
            var employeeBenefitsGeneralInformation = await repository.GetByCompanyId(entity => entity.EmployeeBenefitsPlanId == id);
            if (employeeBenefitsGeneralInformation == null)
            {
                return NotFound();
            }
            return Ok(employeeBenefitsGeneralInformation);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeBenefitsGeneralInformation(int id, EmployeeBenefitsGeneralInformation employeeBenefitsGeneralInformation)
        {
            if (id != employeeBenefitsGeneralInformation.Id)
            {
                return BadRequest();
            }

            var updatedEmployeeBenefitsGeneralInformation = await repository.Update(employeeBenefitsGeneralInformation, id);
            if (updatedEmployeeBenefitsGeneralInformation == null)
            {
                return NotFound();
            }
            return Ok(updatedEmployeeBenefitsGeneralInformation);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeBenefitsGeneralInformation>> PostEmployeeBenefitsGeneralInformation(EmployeeBenefitsGeneralInformation employeeBenefitsGeneralInformation)
        {
            var addedEmployeeBenefitsGeneralInformation = await repository.Add(employeeBenefitsGeneralInformation);
            if (addedEmployeeBenefitsGeneralInformation == null)
            {
                return NotFound();
            }
            return Ok(addedEmployeeBenefitsGeneralInformation);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeBenefitsGeneralInformation(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }
    }
}
