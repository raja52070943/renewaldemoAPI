using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models;
using ClarityImplementation.API.Models.Funding;
using ClarityImplementation.API.Models.Plans;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeBenefitsFundingsController : ControllerBase
    {
        private readonly GenericRepository<EmployeeBenefitsFunding> repository;
        private readonly GenericRepository<EmployeeBenefitsFundingFile> employeeBenefitsFundingFileRepository;

        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public EmployeeBenefitsFundingsController(ClarityDbContext context, IConfiguration configuration)
        {
            repository = new GenericRepository<EmployeeBenefitsFunding>(context);
            employeeBenefitsFundingFileRepository = new GenericRepository<EmployeeBenefitsFundingFile>(context);

            _httpClient = new HttpClient();
            _configuration = configuration;
        }

        // GET: api/EmployeeBenefitsFundings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeBenefitsFunding>>> GetEmployeeBenefitsFundings()
        {
            var EmployeeBenefitsFundings = await repository.GetAll();
            if (EmployeeBenefitsFundings == null)
            {
                return NotFound();
            }
            return Ok(EmployeeBenefitsFundings);
        }

        [HttpGet("ByCompanyId/{id}")]
        public async Task<ActionResult<EmployeeBenefitsFunding>> GetCompanyEmployeeBenefitsFunding(int id)
        {
            var baseURL = _configuration.GetValue<string>("BaseURL");

            var EmployeeBenefitsFunding = await repository.GetByCompanyId(entity => entity.CompanyId == id);
            if (EmployeeBenefitsFunding == null)
            {
                return NotFound();
            }

            var response = await _httpClient.GetAsync(baseURL + "/EmployeeBenefitsFundingFiles/ByEmployeeBenefitsFundingId/" + EmployeeBenefitsFunding.Id);
            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                var ebFundingFileList = JsonConvert.DeserializeObject<List<EmployeeBenefitsFundingFile>>(apiResponse);
                if(ebFundingFileList != null)
                {
                    EmployeeBenefitsFunding.EmployeeBenefitsFundingFiles = ebFundingFileList;
                }
                else
                {
                    EmployeeBenefitsFunding.EmployeeBenefitsFundingFiles = new List<EmployeeBenefitsFundingFile>();
                }
                
            }
            return Ok(EmployeeBenefitsFunding);
        }


        [HttpGet("{id}")]
        //[HttpGet("{id}")]
        public async Task<ActionResult<EmployeeBenefitsFunding>> GetEmployeeBenefitsFunding(int id)
        {
            var baseURL = _configuration.GetValue<string>("BaseURL");

            var employeeBenefitsFunding = await repository.GetById(id);
            if (employeeBenefitsFunding == null)
            {
                return NotFound();
            }

            var fundingResponse = await _httpClient.GetAsync(baseURL + "/EmployeeBenefitsFundingFiles/ByEmployeeBenefitsFundingId/" + id );
            if (fundingResponse.IsSuccessStatusCode)
            {
                string apiResponseFunding = await fundingResponse.Content.ReadAsStringAsync();
                //var address = JsonConvert.DeserializeObject<List<Address>>(apiResponse);
                var employeeBenefitsFundingFiles = JsonConvert.DeserializeObject<List<EmployeeBenefitsFundingFile>>(apiResponseFunding);
                employeeBenefitsFunding.EmployeeBenefitsFundingFiles = employeeBenefitsFundingFiles;
                if (employeeBenefitsFundingFiles != null)
                {
                    employeeBenefitsFunding.EmployeeBenefitsFundingFiles = employeeBenefitsFundingFiles;
                }
                else
                {
                    employeeBenefitsFunding.EmployeeBenefitsFundingFiles = new List<EmployeeBenefitsFundingFile>();
                }
            }
            return Ok(employeeBenefitsFunding);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeBenefitsFunding(int id, EmployeeBenefitsFunding EmployeeBenefitsFunding)
        {
            if (id != EmployeeBenefitsFunding.Id)
            {
                return BadRequest();
            }

            var updatedEmployeeBenefitsFunding = await repository.Update(EmployeeBenefitsFunding, id);
            if (updatedEmployeeBenefitsFunding == null)
            {
                return NotFound();
            }
            return Ok(updatedEmployeeBenefitsFunding);
        }

        [HttpPost]
        public async Task<ActionResult<IActionResult>> PostEmployeeBenefitsFunding(EmployeeBenefitsFunding EmployeeBenefitsFunding)
        {

            var addedEmployeeBenefitsFunding = await repository.Add(EmployeeBenefitsFunding);
            if (addedEmployeeBenefitsFunding == null)
            {
                return NotFound();
            }
            EmployeeBenefitsFundingFile employeeBenefitsFundingFile = new EmployeeBenefitsFundingFile();
            employeeBenefitsFundingFile.EmployeeBenefitsFundingId = addedEmployeeBenefitsFunding.Id;
            await employeeBenefitsFundingFileRepository.Add(employeeBenefitsFundingFile);

            return RedirectToAction("GetEmployeeBenefitsFunding", new { id = addedEmployeeBenefitsFunding.Id });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeBenefitsFunding(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }

        [HttpGet("EmployeeBenefitsFundingProgress/{id}")]
        public async Task<ActionResult<CaseProgress>> GetEmployeeBenefitsFundingProgress(int id)
        {
            var employeeBenefitsFunding = await repository.GetById(id);
            if (employeeBenefitsFunding == null)
            {
                return NotFound();
            }
            return Ok(new CaseProgress { Progress = employeeBenefitsFunding.Progress });
        }
    }
}
