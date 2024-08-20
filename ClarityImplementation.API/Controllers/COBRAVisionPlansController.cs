using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models.Plans.COBRA;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Configuration;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class COBRAVisionPlansController : ControllerBase
    {
        private readonly GenericRepository<COBRAVisionPlan> repository;
        private readonly GenericRepository<VisionCoverageRate> visionCoverageRateRepository;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public COBRAVisionPlansController(ClarityDbContext context, IConfiguration configuration)
        {
            repository = new GenericRepository<COBRAVisionPlan>(context);
            visionCoverageRateRepository = new GenericRepository<VisionCoverageRate>(context);
            _httpClient = new HttpClient();
            _configuration = configuration;
        }
        // GET: api/COBRAVisionPlans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<COBRAVisionPlan>>> GetCOBRAVisionPlans()
        {
            var COBRAVisionPlans = await repository.GetAll();
            if (COBRAVisionPlans == null)
            {
                return NotFound();
            }
            return Ok(COBRAVisionPlans);
        }

        // GET: api/COBRAVisionPlans/5
        [HttpGet("{id}")]
        //[HttpGet("{id}")]
        public async Task<ActionResult<COBRAVisionPlan>> GetCOBRAVisionPlan(int id)
        {
            var baseURL = _configuration.GetValue<string>("BaseURL");

            var COBRAVisionPlan = await repository.GetById(id);
            if (COBRAVisionPlan == null)
            {
                return NotFound();
            }
            if (COBRAVisionPlan.DivisionName != null)
            {
                string[] selectedDivisionNames = COBRAVisionPlan.DivisionName.Split(','); // Splitting into an array of strings directly
                COBRAVisionPlan.SelectedDivisionNames = selectedDivisionNames.ToList(); // Converting the array to a list of strings
            }
            var visionPlanResponse = await _httpClient.GetAsync(baseURL + "/VisionCoverageRates/ByCOBRAVisionId/" + id);
            if (visionPlanResponse.IsSuccessStatusCode)
            {
                string visionPlanAPIResponse = await visionPlanResponse.Content.ReadAsStringAsync();
                //var address = JsonConvert.DeserializeObject<List<Address>>(apiResponse);
                var visionPlanCoverageRate = JsonConvert.DeserializeObject<List<VisionCoverageRate>>(visionPlanAPIResponse);
                COBRAVisionPlan.VisionCoverageRates = visionPlanCoverageRate;
            }
            return Ok(COBRAVisionPlan);
        }


        //[HttpGet("{id}")]
        [HttpGet("ByPlanId/{id}")]
        public async Task<ActionResult<COBRAVisionPlan>> GetCOBRAVisionPlans(int id)
        {
            var baseURL = _configuration.GetValue<string>("BaseURL");

            var COBRAVisionPlans = await repository.GetAllByCompanyId(entity => entity.COBRAPlanId == id);
            if (COBRAVisionPlans == null)
            {
                return NotFound();
            }
            foreach (COBRAVisionPlan visionPlan in COBRAVisionPlans)
            {
                if (visionPlan.DivisionName != null)
                {
                    string[] selectedDivisionNames = visionPlan.DivisionName.Split(','); // Splitting into an array of strings directly
                    visionPlan.SelectedDivisionNames = selectedDivisionNames.ToList(); // Converting the array to a list of strings
                }
                var visionPlanResponse = await _httpClient.GetAsync(baseURL + "/VisionCoverageRates/ByCOBRAVisionId/" + visionPlan.Id);
                if (visionPlanResponse.IsSuccessStatusCode)
                {
                    string visionPlanAPIResponse = await visionPlanResponse.Content.ReadAsStringAsync();
                    //var address = JsonConvert.DeserializeObject<List<Address>>(apiResponse);
                    var visionPlanCoverageRates = JsonConvert.DeserializeObject<List<VisionCoverageRate>>(visionPlanAPIResponse);
                    visionPlan.VisionCoverageRates = visionPlanCoverageRates;
                }
            }
            return Ok(COBRAVisionPlans);
        }

        // PUT: api/COBRAVisionPlans/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCOBRAVisionPlan(int id, COBRAVisionPlan COBRAVisionPlan)
        {
            if (id != COBRAVisionPlan.Id)
            {
                return BadRequest();
            }

            var updatedCOBRAVisionPlan = await repository.Update(COBRAVisionPlan, id);
            if (updatedCOBRAVisionPlan == null)
            {
                return NotFound();
            }
            return Ok(updatedCOBRAVisionPlan);
        }

        // POST: api/COBRAVisionPlans

        [HttpPost]
        public async Task<ActionResult<COBRAVisionPlan>> PostCOBRAVisionPlan(COBRAVisionPlan COBRAVisionPlan)
        {
            // Retrieve the first COBRAVisionPlan
            var firstCOBRAVisionPlan = await repository.GetFirstByCOBRAPlanIdAsync(COBRAVisionPlan.COBRAPlanId);

            List<string> coverageLevelNames = new List<string>();

            if (firstCOBRAVisionPlan != null)
            {
                var existingCoverageRates = await visionCoverageRateRepository.GetByConditionAsync(r => r.VisionPlanId == firstCOBRAVisionPlan.Id);
                coverageLevelNames = existingCoverageRates.Select(r => r.CoverageLevelName).ToList();
            }

            // Add the COBRAVisionPlan to the repository
            var addedCOBRAVisionPlan = await repository.Add(COBRAVisionPlan);
            if (addedCOBRAVisionPlan == null)
            {
                return NotFound();
            }

            // Method to add vision coverage rate
            async Task AddVisionCoverageRateAsync(int visionPlanId, string coverageLevelName = null)
            {
                var visionCoverageRate = new VisionCoverageRate
                {
                    VisionPlanId = visionPlanId,
                    CoverageLevelName = coverageLevelName
                };
                await visionCoverageRateRepository.Add(visionCoverageRate);
            }

            // Add coverage rates based on existing ones or default
            if (coverageLevelNames.Any())
            {
                foreach (var coverageLevelName in coverageLevelNames)
                {
                    await AddVisionCoverageRateAsync(addedCOBRAVisionPlan.Id, coverageLevelName);
                }
            }
            else
            {
                await AddVisionCoverageRateAsync(addedCOBRAVisionPlan.Id);
                await AddVisionCoverageRateAsync(addedCOBRAVisionPlan.Id);
            }

            // Redirect to the GetCOBRAVisionPlan action
            return RedirectToAction("GetCOBRAVisionPlan", new { id = addedCOBRAVisionPlan.Id });
        }


        // DELETE: api/COBRAVisionPlans/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCOBRAVisionPlan(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }
    }
}
