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
    public class COBRAHRAPlansController : ControllerBase
    {
        private readonly GenericRepository<COBRAHRAPlan> repository;
        private readonly GenericRepository<HRACoverageRate> hraCoverageRateRepository;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public COBRAHRAPlansController(ClarityDbContext context, IConfiguration configuration)
        {
            repository = new GenericRepository<COBRAHRAPlan>(context);
            hraCoverageRateRepository = new GenericRepository<HRACoverageRate>(context);
            _httpClient = new HttpClient();
            _configuration = configuration;
        }
        // GET: api/COBRAHRAPlans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<COBRAHRAPlan>>> GetCOBRAHRAPlans()
        {
            var COBRAHRAPlans = await repository.GetAll();
            if (COBRAHRAPlans == null)
            {
                return NotFound();
            }
            return Ok(COBRAHRAPlans);
        }

        // GET: api/COBRAHRAPlans/5
        [HttpGet("{id}")]
        //[HttpGet("{id}")]
        public async Task<ActionResult<COBRAHRAPlan>> GetCOBRAHRAPlan(int id)
        {
            var baseURL = _configuration.GetValue<string>("BaseURL");

            var COBRAHRAPlan = await repository.GetById(id);
            if (COBRAHRAPlan == null)
            {
                return NotFound();
            }
            if (COBRAHRAPlan.DivisionName != null)
            {
                string[] selectedDivisionNames = COBRAHRAPlan.DivisionName.Split(','); // Splitting into an array of strings directly
                COBRAHRAPlan.SelectedDivisionNames = selectedDivisionNames.ToList(); // Converting the array to a list of strings
            }
            var hraPlanResponse = await _httpClient.GetAsync(baseURL + "/HRACoverageRates/ByCOBRAHRAId/" + id);
            if (hraPlanResponse.IsSuccessStatusCode)
            {
                string hraAPIResponse = await hraPlanResponse.Content.ReadAsStringAsync();
                //var address = JsonConvert.DeserializeObject<List<Address>>(apiResponse);
                var hraCoverageRate = JsonConvert.DeserializeObject<List<HRACoverageRate>>(hraAPIResponse);
                COBRAHRAPlan.HRACoverageRates = hraCoverageRate;
            }
            return Ok(COBRAHRAPlan);
        }


        //[HttpGet("{id}")]
        [HttpGet("ByPlanId/{id}")]
        public async Task<ActionResult<COBRAHRAPlan>> GetCOBRAHRAPlans(int id)
        {
            var baseURL = _configuration.GetValue<string>("BaseURL");

            var COBRAHRAPlans = await repository.GetAllByCompanyId(entity => entity.COBRAPlanId == id);
            if (COBRAHRAPlans == null)
            {
                return NotFound();
            }
            foreach (COBRAHRAPlan hraPlan in COBRAHRAPlans)
            {
                if (hraPlan.DivisionName != null)
                {
                    string[] selectedDivisionNames = hraPlan.DivisionName.Split(','); // Splitting into an array of strings directly
                    hraPlan.SelectedDivisionNames = selectedDivisionNames.ToList(); // Converting the array to a list of strings
                }
                var hraPlanResponse = await _httpClient.GetAsync(baseURL + "/HRACoverageRates/ByCOBRAHRAId/" + hraPlan.Id);
                if (hraPlanResponse.IsSuccessStatusCode)
                {
                    string hraAPIResponse = await hraPlanResponse.Content.ReadAsStringAsync();
                    //var address = JsonConvert.DeserializeObject<List<Address>>(apiResponse);
                    var hraCoverageRates = JsonConvert.DeserializeObject<List<HRACoverageRate>>(hraAPIResponse);
                    hraPlan.HRACoverageRates = hraCoverageRates;
                }
            }
            return Ok(COBRAHRAPlans);
        }

        // PUT: api/COBRAHRAPlans/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCOBRAHRAPlan(int id, COBRAHRAPlan COBRAHRAPlan)
        {
            if (id != COBRAHRAPlan.Id)
            {
                return BadRequest();
            }

            var updatedCOBRAHRAPlan = await repository.Update(COBRAHRAPlan, id);
            if (updatedCOBRAHRAPlan == null)
            {
                return NotFound();
            }
            return Ok(updatedCOBRAHRAPlan);
        }

        // POST: api/COBRAHRAPlans

        [HttpPost]
        public async Task<ActionResult<COBRAHRAPlan>> PostCOBRAHRAPlan(COBRAHRAPlan COBRAHRAPlan)
        {
            // Retrieve the first COBRAHRAPlan
            var firstCOBRAHRAPlan = await repository.GetFirstByCOBRAPlanIdAsync(COBRAHRAPlan.COBRAPlanId);

            List<string> coverageLevelNames = new List<string>();

            if (firstCOBRAHRAPlan != null)
            {
                var existingCoverageRates = await hraCoverageRateRepository.GetByConditionAsync(r => r.HRAPlanId == firstCOBRAHRAPlan.Id);
                coverageLevelNames = existingCoverageRates.Select(r => r.CoverageLevelName).ToList();
            }

            // Add the COBRAHRAPlan to the repository
            var addedCOBRAHRAPlan = await repository.Add(COBRAHRAPlan);
            if (addedCOBRAHRAPlan == null)
            {
                return NotFound();
            }

            // Method to add HRA coverage rate
            async Task AddHRACoverageRateAsync(int hraPlanId, string coverageLevelName = null)
            {
                var hraCoverageRate = new HRACoverageRate
                {
                    HRAPlanId = hraPlanId,
                    CoverageLevelName = coverageLevelName
                };
                await hraCoverageRateRepository.Add(hraCoverageRate);
            }

            // Add coverage rates based on existing ones or default
            if (coverageLevelNames.Any())
            {
                foreach (var coverageLevelName in coverageLevelNames)
                {
                    await AddHRACoverageRateAsync(addedCOBRAHRAPlan.Id, coverageLevelName);
                }
            }
            else
            {
                await AddHRACoverageRateAsync(addedCOBRAHRAPlan.Id);
                await AddHRACoverageRateAsync(addedCOBRAHRAPlan.Id);
            }

            // Redirect to the GetCOBRAHRAPlan action
            return RedirectToAction("GetCOBRAHRAPlan", new { id = addedCOBRAHRAPlan.Id });
        }


        // DELETE: api/COBRAHRAPlans/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCOBRAHRAPlan(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }
    }
}
