using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models.Plans.COBRA;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class COBRADentalPlansController : ControllerBase
    {
        private readonly GenericRepository<COBRADentalPlan> repository;
        private readonly GenericRepository<DentalCoverageRate> dentalCoverageRateRepository;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public COBRADentalPlansController(ClarityDbContext context, IConfiguration configuration)
        {
            repository = new GenericRepository<COBRADentalPlan>(context);
            dentalCoverageRateRepository = new GenericRepository<DentalCoverageRate>(context);
            _httpClient = new HttpClient();
            _configuration = configuration;
        }
        // GET: api/COBRADentalPlans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<COBRADentalPlan>>> GetCOBRADentalPlans()
        {
            var COBRADentalPlans = await repository.GetAll();
            if (COBRADentalPlans == null)
            {
                return NotFound();
            }
            return Ok(COBRADentalPlans);
        }

        // GET: api/COBRADentalPlans/5
        [HttpGet("{id}")]
        //[HttpGet("{id}")]
        public async Task<ActionResult<COBRADentalPlan>> GetCOBRADentalPlan(int id)
        {
            var baseURL = _configuration.GetValue<string>("BaseURL");

            var COBRADentalPlan = await repository.GetById(id);
            if (COBRADentalPlan == null)
            {
                return NotFound();
            }
            if (COBRADentalPlan.DivisionName != null)
            {
                string[] selectedDivisionNames = COBRADentalPlan.DivisionName.Split(','); // Splitting into an array of strings directly
                COBRADentalPlan.SelectedDivisionNames = selectedDivisionNames.ToList(); // Converting the array to a list of strings
            }
            var dentalPlanResponse = await _httpClient.GetAsync(baseURL + "/DentalCoverageRates/ByCOBRADentalPlanId/" + id);
            if (dentalPlanResponse.IsSuccessStatusCode)
            {
                string dentalPlanAPIResponse = await dentalPlanResponse.Content.ReadAsStringAsync();
                //var address = JsonConvert.DeserializeObject<List<Address>>(apiResponse);
                var dentalPlanCoverageRate = JsonConvert.DeserializeObject<List<DentalCoverageRate>>(dentalPlanAPIResponse);
                COBRADentalPlan.DentalCoverageRates = dentalPlanCoverageRate;
            }
            return Ok(COBRADentalPlan);
        }


        //[HttpGet("{id}")]
        [HttpGet("ByPlanId/{id}")]
        public async Task<ActionResult<COBRADentalPlan>> GetCOBRADentalPlansByPlanId(int id)
        {
            var baseURL = _configuration.GetValue<string>("BaseURL");

            var COBRADentalPlans = await repository.GetAllByCompanyId(entity => entity.COBRAPlanId == id);
            if (COBRADentalPlans == null)
            {
                return NotFound();
            }

            foreach (COBRADentalPlan dentalPlan in COBRADentalPlans)
            {
                if (dentalPlan.DivisionName != null)
                {
                    string[] selectedDivisionNames = dentalPlan.DivisionName.Split(','); // Splitting into an array of strings directly
                    dentalPlan.SelectedDivisionNames = selectedDivisionNames.ToList(); // Converting the array to a list of strings
                }
                var dentalPlanResponse = await _httpClient.GetAsync(baseURL + "/DentalCoverageRates/ByCOBRADentalPlanId/" + dentalPlan.Id);
                if (dentalPlanResponse.IsSuccessStatusCode)
                {
                    string dentalPlanAPIResponse = await dentalPlanResponse.Content.ReadAsStringAsync();
                    //var address = JsonConvert.DeserializeObject<List<Address>>(apiResponse);
                    var dentalPlanCoverageRates = JsonConvert.DeserializeObject<List<DentalCoverageRate>>(dentalPlanAPIResponse);
                    dentalPlan.DentalCoverageRates = dentalPlanCoverageRates;
                }
            }
            return Ok(COBRADentalPlans);
        }

        // PUT: api/COBRADentalPlans/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCOBRADentalPlan(int id, COBRADentalPlan COBRADentalPlan)
        {
            if (id != COBRADentalPlan.Id)
            {
                return BadRequest();
            }

            var updatedCOBRADentalPlan = await repository.Update(COBRADentalPlan, id);
            if (updatedCOBRADentalPlan == null)
            {
                return NotFound();
            }
            return Ok(updatedCOBRADentalPlan);
        }

        // POST: api/COBRADentalPlans

        [HttpPost]
        public async Task<ActionResult<COBRADentalPlan>> PostCOBRADentalPlan(COBRADentalPlan COBRADentalPlan)
        {
            // Retrieve the first COBRADentalPlan
            var firstCOBRADentalPlan = await repository.GetFirstByCOBRAPlanIdAsync(COBRADentalPlan.COBRAPlanId);

            List<string> coverageLevelNames = new List<string>();

            if (firstCOBRADentalPlan != null)
            {
                var existingCoverageRates = await dentalCoverageRateRepository.GetByConditionAsync(r => r.DentalPlanId == firstCOBRADentalPlan.Id);
                coverageLevelNames = existingCoverageRates.Select(r => r.CoverageLevelName).ToList();
            }

            // Add the COBRADentalPlan to the repository
            var addedCOBRADentalPlan = await repository.Add(COBRADentalPlan);
            if (addedCOBRADentalPlan == null)
            {
                return NotFound();
            }

            // Method to add dental coverage rate
            async Task AddDentalCoverageRateAsync(int dentalPlanId, string coverageLevelName = null)
            {
                var dentalCoverageRate = new DentalCoverageRate
                {
                    DentalPlanId = dentalPlanId,
                    CoverageLevelName = coverageLevelName
                };
                await dentalCoverageRateRepository.Add(dentalCoverageRate);
            }

            // Add coverage rates based on existing ones or default
            if (coverageLevelNames.Any())
            {
                foreach (var coverageLevelName in coverageLevelNames)
                {
                    await AddDentalCoverageRateAsync(addedCOBRADentalPlan.Id, coverageLevelName);
                }
            }
            else
            {
                await AddDentalCoverageRateAsync(addedCOBRADentalPlan.Id);
                await AddDentalCoverageRateAsync(addedCOBRADentalPlan.Id);
            }

            // Redirect to the GetCOBRADentalPlan action
            return RedirectToAction("GetCOBRADentalPlan", new { id = addedCOBRADentalPlan.Id });
        }


        // DELETE: api/COBRADentalPlans/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCOBRADentalPlan(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }
    }
}
