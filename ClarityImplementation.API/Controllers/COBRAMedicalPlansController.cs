using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models;
using ClarityImplementation.API.Models.Plans;
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
    public class COBRAMedicalPlansController : ControllerBase
    {
        private readonly GenericRepository<COBRAMedicalPlan> repository;
        private readonly GenericRepository<MedicalPlanCoverageRate> medicalPlanCoverageRateRepository;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public COBRAMedicalPlansController(ClarityDbContext context, IConfiguration configuration)
        {
            repository = new GenericRepository<COBRAMedicalPlan>(context);
            medicalPlanCoverageRateRepository = new GenericRepository<MedicalPlanCoverageRate>(context);
            _httpClient = new HttpClient();
            _configuration = configuration;
        }
        // GET: api/COBRAMedicalPlans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<COBRAMedicalPlan>>> GetCOBRAMedicalPlans()
        {
            var COBRAMedicalPlans = await repository.GetAll();
            if (COBRAMedicalPlans == null)
            {
                return NotFound();
            }
            return Ok(COBRAMedicalPlans);
        }

        // GET: api/COBRAMedicalPlans/5
        [HttpGet("{id}")]
        //[HttpGet("{id}")]
        public async Task<ActionResult<COBRAMedicalPlan>> GetCOBRAMedicalPlan(int id)
        {
            var baseURL = _configuration.GetValue<string>("BaseURL");

            var COBRAMedicalPlan = await repository.GetById(id);
            if (COBRAMedicalPlan == null)
            {
                return NotFound();
            }
            if (COBRAMedicalPlan.DivisionName != null)
            {
                string[] selectedDivisionNames = COBRAMedicalPlan.DivisionName.Split(','); // Splitting into an array of strings directly
                COBRAMedicalPlan.SelectedDivisionNames = selectedDivisionNames.ToList(); // Converting the array to a list of strings
            }
            var medicalPlanResponse = await _httpClient.GetAsync(baseURL + "/MedicalPlanCoverageRates/ByCOBRAMedicalPlanId/" + id);
            if (medicalPlanResponse.IsSuccessStatusCode)
            {
                string medicalPlanAPIResponse = await medicalPlanResponse.Content.ReadAsStringAsync();
                //var address = JsonConvert.DeserializeObject<List<Address>>(apiResponse);
                var medicalPlanCoverageRate = JsonConvert.DeserializeObject<List<MedicalPlanCoverageRate>>(medicalPlanAPIResponse);
                COBRAMedicalPlan.MedicalPlanCoverageRates = medicalPlanCoverageRate;
            }
            return Ok(COBRAMedicalPlan);
        }


        //[HttpGet("{id}")]
        [HttpGet("ByPlanId/{id}")]
        public async Task<ActionResult<COBRAMedicalPlan>> GetCOBRAMedicalPlansByPlanId(int id)
        {
            var baseURL = _configuration.GetValue<string>("BaseURL");

            var COBRAMedicalPlans = await repository.GetAllByCompanyId(entity => entity.COBRAPlanId == id);
            if (COBRAMedicalPlans == null)
            {
                return NotFound();
            }
            
            foreach (COBRAMedicalPlan medicalPlan in COBRAMedicalPlans)
            {
                if (medicalPlan.DivisionName != null)
                {
                    string[] selectedDivisionNames = medicalPlan.DivisionName.Split(','); // Splitting into an array of strings directly
                    medicalPlan.SelectedDivisionNames = selectedDivisionNames.ToList(); // Converting the array to a list of strings
                }
                var medicalPlanResponse = await _httpClient.GetAsync(baseURL + "/MedicalPlanCoverageRates/ByCOBRAMedicalPlanId/" + medicalPlan.Id);
                if (medicalPlanResponse.IsSuccessStatusCode)
                {
                    string medicalPlanAPIResponse = await medicalPlanResponse.Content.ReadAsStringAsync();
                    //var address = JsonConvert.DeserializeObject<List<Address>>(apiResponse);
                    var medicalPlanCoverageRates = JsonConvert.DeserializeObject<List<MedicalPlanCoverageRate>>(medicalPlanAPIResponse);
                    medicalPlan.MedicalPlanCoverageRates = medicalPlanCoverageRates;
                }
            }
            return Ok(COBRAMedicalPlans);
        }

        // PUT: api/COBRAMedicalPlans/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCOBRAMedicalPlan(int id, COBRAMedicalPlan COBRAMedicalPlan)
        {
            if (id != COBRAMedicalPlan.Id)
            {
                return BadRequest();
            }

            var updatedCOBRAMedicalPlan = await repository.Update(COBRAMedicalPlan, id);
            if (updatedCOBRAMedicalPlan == null)
            {
                return NotFound();
            }
            return Ok(updatedCOBRAMedicalPlan);
        }

        // POST: api/COBRAMedicalPlans

        [HttpPost]
        public async Task<ActionResult<COBRAMedicalPlan>> PostCOBRAMedicalPlan(COBRAMedicalPlan COBRAMedicalPlan)
        {
            // Retrieve the first COBRAMedicalPlan
            var firstCOBRAMedicalPlan = await repository.GetFirstByCOBRAPlanIdAsync(COBRAMedicalPlan.COBRAPlanId);

            List<string> coverageLevelNames = new List<string>();

            if (firstCOBRAMedicalPlan != null)
            {
                
                var existingCoverageRates = await medicalPlanCoverageRateRepository.GetByConditionAsync(r => r.COBRAMedicalPlanId == firstCOBRAMedicalPlan.Id);
                coverageLevelNames = existingCoverageRates.Select(r => r.CoverageLevelName).ToList();
            }

            
            var addedCOBRAMedicalPlan = await repository.Add(COBRAMedicalPlan);
            if (addedCOBRAMedicalPlan == null)
            {
                return NotFound();
            }


            if (coverageLevelNames.Any())
            {
                foreach (var coverageLevelName in coverageLevelNames)
                {
                    MedicalPlanCoverageRate medicalPlanCoverageRate = new MedicalPlanCoverageRate
                    {
                        COBRAMedicalPlanId = addedCOBRAMedicalPlan.Id,
                        CoverageLevelName = coverageLevelName
                    };
                    await medicalPlanCoverageRateRepository.Add(medicalPlanCoverageRate);
                }
            }
            else
            {
                MedicalPlanCoverageRate medicalPlanCoverageRate1 = new MedicalPlanCoverageRate
                {
                    COBRAMedicalPlanId = addedCOBRAMedicalPlan.Id
                };
                await medicalPlanCoverageRateRepository.Add(medicalPlanCoverageRate1);

                MedicalPlanCoverageRate medicalPlanCoverageRate2 = new MedicalPlanCoverageRate
                {
                    COBRAMedicalPlanId = addedCOBRAMedicalPlan.Id
                };
                await medicalPlanCoverageRateRepository.Add(medicalPlanCoverageRate2);
            }

            return RedirectToAction("GetCOBRAMedicalPlan", new { id = addedCOBRAMedicalPlan.Id });
        }


        // DELETE: api/COBRAMedicalPlans/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCOBRAMedicalPlan(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }
    }
}
