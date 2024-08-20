using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models.Plans.COBRA;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class COBRAEAPPlansController : Controller
    {
        private readonly GenericRepository<COBRAEAPPlan> repository;
        private readonly GenericRepository<EAPCoverageRate> eapCoverageRateRepository;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public COBRAEAPPlansController(ClarityDbContext context, IConfiguration configuration)
        {
            repository = new GenericRepository<COBRAEAPPlan>(context);
            eapCoverageRateRepository = new GenericRepository<EAPCoverageRate>(context);
            _httpClient = new HttpClient();
            _configuration = configuration;
        }
        // GET: api/COBRAEAPPlans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<COBRAEAPPlan>>> GetCOBRAEAPPlans()
        {
            var COBRAEAPPlans = await repository.GetAll();
            if (COBRAEAPPlans == null)
            {
                return NotFound();
            }
            return Ok(COBRAEAPPlans);
        }

        // GET: api/COBRAEAPPlans/5
        [HttpGet("{id}")]
        //[HttpGet("{id}")]
        public async Task<ActionResult<COBRAEAPPlan>> GetCOBRAEAPPlan(int id)
        {
            var baseURL = _configuration.GetValue<string>("BaseURL");

            var COBRAEAPPlan = await repository.GetById(id);
            if (COBRAEAPPlan == null)
            {
                return NotFound();
            }
            if (COBRAEAPPlan.DivisionName != null)
            {
                string[] selectedDivisionNames = COBRAEAPPlan.DivisionName.Split(','); // Splitting into an array of strings directly
                COBRAEAPPlan.SelectedDivisionNames = selectedDivisionNames.ToList(); // Converting the array to a list of strings
            }
            var eapPlanResponse = await _httpClient.GetAsync(baseURL + "/EAPCoverageRates/ByCOBRAEAPId/" + id);
            if (eapPlanResponse.IsSuccessStatusCode)
            {
                string eapAPIResponse = await eapPlanResponse.Content.ReadAsStringAsync();
                //var address = JsonConvert.DeserializeObject<List<Address>>(apiResponse);
                var eapCoverageRate = JsonConvert.DeserializeObject<List<EAPCoverageRate>>(eapAPIResponse);
                COBRAEAPPlan.EAPCoverageRates = eapCoverageRate;
            }
            return Ok(COBRAEAPPlan);
        }


        //[HttpGet("{id}")]
        [HttpGet("ByPlanId/{id}")]
        public async Task<ActionResult<COBRAEAPPlan>> GetCOBRAEAPPlans(int id)
        {
            var baseURL = _configuration.GetValue<string>("BaseURL");

            var COBRAEAPPlans = await repository.GetAllByCompanyId(entity => entity.COBRAPlanId == id);
            if (COBRAEAPPlans == null)
            {
                return NotFound();
            }
            foreach (COBRAEAPPlan eapPlan in COBRAEAPPlans)
            {
                if (eapPlan.DivisionName != null)
                {
                    string[] selectedDivisionNames = eapPlan.DivisionName.Split(','); // Splitting into an array of strings directly
                    eapPlan.SelectedDivisionNames = selectedDivisionNames.ToList(); // Converting the array to a list of strings
                }
                var eapPlanResponse = await _httpClient.GetAsync(baseURL + "/EAPCoverageRates/ByCOBRAEAPId/" + eapPlan.Id);
                if (eapPlanResponse.IsSuccessStatusCode)
                {
                    string eapAPIResponse = await eapPlanResponse.Content.ReadAsStringAsync();
                    //var address = JsonConvert.DeserializeObject<List<Address>>(apiResponse);
                    var eapCoverageRates = JsonConvert.DeserializeObject<List<EAPCoverageRate>>(eapAPIResponse);
                    eapPlan.EAPCoverageRates = eapCoverageRates;
                }
            }
            return Ok(COBRAEAPPlans);
        }

        // PUT: api/COBRAFSAPlans/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCOBRAEAPPlan(int id, COBRAEAPPlan COBRAEAPPlan)
        {
            if (id != COBRAEAPPlan.Id)
            {
                return BadRequest();
            }

            var updatedCOBRAEAPPlan = await repository.Update(COBRAEAPPlan, id);
            if (updatedCOBRAEAPPlan == null)
            {
                return NotFound();
            }
            return Ok(updatedCOBRAEAPPlan);
        }

        // POST: api/COBRAEAPPlans

        [HttpPost]
        public async Task<ActionResult<COBRAEAPPlan>> PostCOBRAEAPPlan(COBRAEAPPlan COBRAEAPPlan)
        {
            // Retrieve the first COBRAEAPPlan
            var firstCOBRAEAPPlan = await repository.GetFirstByCOBRAPlanIdAsync(COBRAEAPPlan.COBRAPlanId);

            List<string> coverageLevelNames = new List<string>();

            if (firstCOBRAEAPPlan != null)
            {
                var existingCoverageRates = await eapCoverageRateRepository.GetByConditionAsync(r => r.EAPPlanId == firstCOBRAEAPPlan.Id);
                coverageLevelNames = existingCoverageRates.Select(r => r.CoverageLevelName).ToList();
            }

            // Add the COBRAEAPPlan to the repository
            var addedCOBRAEAPPlan = await repository.Add(COBRAEAPPlan);
            if (addedCOBRAEAPPlan == null)
            {
                return NotFound();
            }

            // Method to add EAP coverage rate
            async Task AddEAPCoverageRateAsync(int eapPlanId, string coverageLevelName = null)
            {
                var eapCoverageRate = new EAPCoverageRate
                {
                    EAPPlanId = eapPlanId,
                    CoverageLevelName = coverageLevelName
                };
                await eapCoverageRateRepository.Add(eapCoverageRate);
            }

            // Add coverage rates based on existing ones or default
            if (coverageLevelNames.Any())
            {
                foreach (var coverageLevelName in coverageLevelNames)
                {
                    await AddEAPCoverageRateAsync(addedCOBRAEAPPlan.Id, coverageLevelName);
                }
            }
            else
            {
                await AddEAPCoverageRateAsync(addedCOBRAEAPPlan.Id);
                await AddEAPCoverageRateAsync(addedCOBRAEAPPlan.Id);
            }

            // Redirect to the GetCOBRAEAPPlan action
            return RedirectToAction("GetCOBRAEAPPlan", new { id = addedCOBRAEAPPlan.Id });
        }


        // DELETE: api/COBRAEAPPlans/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCOBRAEAPPlan(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }
    }
}
