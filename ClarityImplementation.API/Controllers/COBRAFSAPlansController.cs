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
    public class COBRAFSAPlansController : ControllerBase
    {
        private readonly GenericRepository<COBRAFSAPlan> repository;
        private readonly GenericRepository<FSACoverageRate> fsaCoverageRateRepository;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public COBRAFSAPlansController(ClarityDbContext context, IConfiguration configuration)
        {
            repository = new GenericRepository<COBRAFSAPlan>(context);
            fsaCoverageRateRepository = new GenericRepository<FSACoverageRate>(context);
            _httpClient = new HttpClient();
            _configuration = configuration;
        }
        // GET: api/COBRAFSAPlans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<COBRAFSAPlan>>> GetCOBRAFSAPlans()
        {
            var COBRAFSAPlans = await repository.GetAll();
            if (COBRAFSAPlans == null)
            {
                return NotFound();
            }
            return Ok(COBRAFSAPlans);
        }

        // GET: api/COBRAFSAPlans/5
        [HttpGet("{id}")]
        //[HttpGet("{id}")]
        public async Task<ActionResult<COBRAFSAPlan>> GetCOBRAFSAPlan(int id)
        {
            var baseURL = _configuration.GetValue<string>("BaseURL");

            var COBRAFSAPlan = await repository.GetById(id);
            if (COBRAFSAPlan == null)
            {
                return NotFound();
            }
            if (COBRAFSAPlan.DivisionName != null)
            {
                string[] selectedDivisionNames = COBRAFSAPlan.DivisionName.Split(','); // Splitting into an array of strings directly
                COBRAFSAPlan.SelectedDivisionNames = selectedDivisionNames.ToList(); // Converting the array to a list of strings
            }
            var fsaPlanResponse = await _httpClient.GetAsync(baseURL + "/FSACoverageRates/ByCOBRAFSAId/" + id);
            if (fsaPlanResponse.IsSuccessStatusCode)
            {
                string fsaAPIResponse = await fsaPlanResponse.Content.ReadAsStringAsync();
                //var address = JsonConvert.DeserializeObject<List<Address>>(apiResponse);
                var fsaCoverageRate = JsonConvert.DeserializeObject<List<FSACoverageRate>>(fsaAPIResponse);
                COBRAFSAPlan.FSACoverageRates = fsaCoverageRate;
            }
            return Ok(COBRAFSAPlan);
        }


        //[HttpGet("{id}")]
        [HttpGet("ByPlanId/{id}")]
        public async Task<ActionResult<COBRAFSAPlan>> GetCOBRAFSAPlans(int id)
        {
            var baseURL = _configuration.GetValue<string>("BaseURL");

            var COBRAFSAPlans = await repository.GetAllByCompanyId(entity => entity.COBRAPlanId == id);
            if (COBRAFSAPlans == null)
            {
                return NotFound();
            }

            foreach (COBRAFSAPlan fsaPlan in COBRAFSAPlans)
            {
                if (fsaPlan.DivisionName != null)
                {
                    string[] selectedDivisionNames = fsaPlan.DivisionName.Split(','); // Splitting into an array of strings directly
                    fsaPlan.SelectedDivisionNames = selectedDivisionNames.ToList(); // Converting the array to a list of strings
                }
                var fsaPlanResponse = await _httpClient.GetAsync(baseURL + "/FSACoverageRates/ByCOBRAFSAId/" + fsaPlan.Id);
                if (fsaPlanResponse.IsSuccessStatusCode)
                {
                    string fsaAPIResponse = await fsaPlanResponse.Content.ReadAsStringAsync();
                    //var address = JsonConvert.DeserializeObject<List<Address>>(apiResponse);
                    var fsaCoverageRates = JsonConvert.DeserializeObject<List<FSACoverageRate>>(fsaAPIResponse);
                    fsaPlan.FSACoverageRates = fsaCoverageRates;
                }
            }
            return Ok(COBRAFSAPlans);
        }

        // PUT: api/COBRAFSAPlans/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCOBRAFSAPlan(int id, COBRAFSAPlan COBRAFSAPlan)
        {
            if (id != COBRAFSAPlan.Id)
            {
                return BadRequest();
            }

            var updatedCOBRAFSAPlan = await repository.Update(COBRAFSAPlan, id);
            if (updatedCOBRAFSAPlan == null)
            {
                return NotFound();
            }
            return Ok(updatedCOBRAFSAPlan);
        }

        // POST: api/COBRAFSAPlans

        [HttpPost]
        public async Task<ActionResult<COBRAFSAPlan>> PostCOBRAFSAPlan(COBRAFSAPlan COBRAFSAPlan)
        {
            // Retrieve the first COBRAFSAPlan
            var firstCOBRAFSAPlan = await repository.GetFirstByCOBRAPlanIdAsync(COBRAFSAPlan.COBRAPlanId);

            List<string> coverageLevelNames = new List<string>();

            if (firstCOBRAFSAPlan != null)
            {
                var existingCoverageRates = await fsaCoverageRateRepository.GetByConditionAsync(r => r.FSAPlanId == firstCOBRAFSAPlan.Id);
                coverageLevelNames = existingCoverageRates.Select(r => r.CoverageLevelName).ToList();
            }

            // Add the COBRAFSAPlan to the repository
            var addedCOBRAFSAPlan = await repository.Add(COBRAFSAPlan);
            if (addedCOBRAFSAPlan == null)
            {
                return NotFound();
            }

            // Method to add FSA coverage rate
            async Task AddFSACoverageRateAsync(int fsaPlanId, string coverageLevelName = null)
            {
                var fsaCoverageRate = new FSACoverageRate
                {
                    FSAPlanId = fsaPlanId,
                    CoverageLevelName = coverageLevelName
                };
                await fsaCoverageRateRepository.Add(fsaCoverageRate);
            }

            // Add coverage rates based on existing ones or default
            if (coverageLevelNames.Any())
            {
                foreach (var coverageLevelName in coverageLevelNames)
                {
                    await AddFSACoverageRateAsync(addedCOBRAFSAPlan.Id, coverageLevelName);
                }
            }
            else
            {
                await AddFSACoverageRateAsync(addedCOBRAFSAPlan.Id);
                await AddFSACoverageRateAsync(addedCOBRAFSAPlan.Id);
            }

            // Redirect to the GetCOBRAFSAPlan action
            return RedirectToAction("GetCOBRAFSAPlan", new { id = addedCOBRAFSAPlan.Id });
        }


        // DELETE: api/COBRAFSAPlans/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCOBRAFSAPlan(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }
    }
}
