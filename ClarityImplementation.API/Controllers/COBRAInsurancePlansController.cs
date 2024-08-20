using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models.Plans.COBRA;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class COBRAInsurancePlansController : ControllerBase
    {
        private readonly GenericRepository<COBRAInsurancePlan> repository;
        private readonly GenericRepository<InsurancePlanCoverageRate> insurancePlanCoverageRateRepository;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public COBRAInsurancePlansController(ClarityDbContext context, IConfiguration configuration)
        {
            repository = new GenericRepository<COBRAInsurancePlan>(context);
            insurancePlanCoverageRateRepository = new GenericRepository<InsurancePlanCoverageRate>(context);
            _httpClient = new HttpClient();
            _configuration = configuration;
        }
        // GET: api/COBRAInsurancePlans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<COBRAInsurancePlan>>> GetCOBRAInsurancePlans()
        {
            var COBRAInsurancePlans = await repository.GetAll();
            if (COBRAInsurancePlans == null)
            {
                return NotFound();
            }
            return Ok(COBRAInsurancePlans);
        }

        // GET: api/COBRAInsurancePlans/5
        [HttpGet("{id}")]
        //[HttpGet("{id}")]
        public async Task<ActionResult<COBRAInsurancePlan>> GetCOBRAInsurancePlan(int id)
        {
            var baseURL = _configuration.GetValue<string>("BaseURL");

            var COBRAInsurancePlan = await repository.GetById(id);
            if (COBRAInsurancePlan == null)
            {
                return NotFound();
            }
            if (COBRAInsurancePlan.DivisionName != null)
            {
                string[] selectedDivisionNames = COBRAInsurancePlan.DivisionName.Split(','); // Splitting into an array of strings directly
                COBRAInsurancePlan.SelectedDivisionNames = selectedDivisionNames.ToList(); // Converting the array to a list of strings
            }
            var insurancePlanResponse = await _httpClient.GetAsync(baseURL + "/InsurancePlanCoverageRates/ByCOBRAInsurancePlanId/" + id);
            if (insurancePlanResponse.IsSuccessStatusCode)
            {
                string insurancePlanAPIResponse = await insurancePlanResponse.Content.ReadAsStringAsync();
                //var address = JsonConvert.DeserializeObject<List<Address>>(apiResponse);
                var insurancePlanCoverageRate = JsonConvert.DeserializeObject<List<InsurancePlanCoverageRate>>(insurancePlanAPIResponse);
                COBRAInsurancePlan.InsurancePlanCoverageRates = insurancePlanCoverageRate;
            }
            return Ok(COBRAInsurancePlan);
        }


        //[HttpGet("{id}")]
        [HttpGet("ByPlanId/{id}")]
        public async Task<ActionResult<COBRAInsurancePlan>> GetCOBRAInsurancePlans(int id)
        {
            var baseURL = _configuration.GetValue<string>("BaseURL");

            var COBRAInsurancePlans = await repository.GetAllByCompanyId(entity => entity.COBRAPlanId == id);
            if (COBRAInsurancePlans == null)
            {
                return NotFound();
            }
            foreach (COBRAInsurancePlan insurancePlan in COBRAInsurancePlans)
            {
                if (insurancePlan.DivisionName != null)
                {
                    string[] selectedDivisionNames = insurancePlan.DivisionName.Split(','); // Splitting into an array of strings directly
                    insurancePlan.SelectedDivisionNames = selectedDivisionNames.ToList(); // Converting the array to a list of strings
                }
                var insurancePlanResponse = await _httpClient.GetAsync(baseURL + "/InsurancePlanCoverageRates/ByCOBRAInsurancePlanId/" + insurancePlan.Id);
                if (insurancePlanResponse.IsSuccessStatusCode)
                {
                    string insurancePlanAPIResponse = await insurancePlanResponse.Content.ReadAsStringAsync();
                    //var address = JsonConvert.DeserializeObject<List<Address>>(apiResponse);
                    var insurancePlanCoverageRates = JsonConvert.DeserializeObject<List<InsurancePlanCoverageRate>>(insurancePlanAPIResponse);
                    insurancePlan.InsurancePlanCoverageRates = insurancePlanCoverageRates;
                }
            }
            return Ok(COBRAInsurancePlans);
        }

        // PUT: api/COBRAInsurancePlans/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCOBRAInsurancePlan(int id, COBRAInsurancePlan COBRAInsurancePlan)
        {
            if (id != COBRAInsurancePlan.Id)
            {
                return BadRequest();
            }

            var updatedCOBRAInsurancePlan = await repository.Update(COBRAInsurancePlan, id);
            if (updatedCOBRAInsurancePlan == null)
            {
                return NotFound();
            }
            return Ok(updatedCOBRAInsurancePlan);
        }

        // POST: api/COBRAInsurancePlans

        [HttpPost]
        public async Task<ActionResult<COBRAInsurancePlan>> PostCOBRAInsurancePlan(COBRAInsurancePlan COBRAInsurancePlan)
        {
            // Retrieve the first COBRAInsurancePlan
            var firstCOBRAInsurancePlan = await repository.GetFirstByCOBRAPlanIdAsync(COBRAInsurancePlan.COBRAPlanId);

            List<string> coverageLevelNames = new List<string>();

            if (firstCOBRAInsurancePlan != null)
            {
                var existingCoverageRates = await insurancePlanCoverageRateRepository.GetByConditionAsync(r => r.InsurancePlanId == firstCOBRAInsurancePlan.Id);
                coverageLevelNames = existingCoverageRates.Select(r => r.CoverageLevelName).ToList();
            }

            // Add the COBRAInsurancePlan to the repository
            var addedCOBRAInsurancePlan = await repository.Add(COBRAInsurancePlan);
            if (addedCOBRAInsurancePlan == null)
            {
                return NotFound();
            }

            // Method to add insurance coverage rate
            async Task AddInsurancePlanCoverageRateAsync(int insurancePlanId, string coverageLevelName = null)
            {
                var insurancePlanCoverageRate = new InsurancePlanCoverageRate
                {
                    InsurancePlanId = insurancePlanId,
                    CoverageLevelName = coverageLevelName
                };
                await insurancePlanCoverageRateRepository.Add(insurancePlanCoverageRate);
            }

            // Add coverage rates based on existing ones or default
            if (coverageLevelNames.Any())
            {
                foreach (var coverageLevelName in coverageLevelNames)
                {
                    await AddInsurancePlanCoverageRateAsync(addedCOBRAInsurancePlan.Id, coverageLevelName);
                }
            }
            else
            {
                await AddInsurancePlanCoverageRateAsync(addedCOBRAInsurancePlan.Id);
                await AddInsurancePlanCoverageRateAsync(addedCOBRAInsurancePlan.Id);
            }

            // Redirect to the GetCOBRAInsurancePlan action
            return RedirectToAction("GetCOBRAInsurancePlan", new { id = addedCOBRAInsurancePlan.Id });
        }


        // DELETE: api/COBRAInsurancePlans/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCOBRAInsurancePlan(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }
    }
}
