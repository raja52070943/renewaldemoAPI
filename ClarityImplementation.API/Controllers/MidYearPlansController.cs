using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models;
using ClarityImplementation.API.Models.Plans;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MidYearPlansController : ControllerBase
    {
        private readonly GenericRepository<MidYearPlan> repository;
        private readonly GenericRepository<EmployeeBenefitsPlan> ebpRepo;
        private readonly GenericRepository<CompanyDetails> companyDetailsRepository;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public MidYearPlansController(ClarityDbContext context , IConfiguration configuration)
        {
            repository = new GenericRepository<MidYearPlan>(context);
            ebpRepo = new GenericRepository<EmployeeBenefitsPlan>(context);
            companyDetailsRepository = new GenericRepository<CompanyDetails>(context);

            _httpClient = new HttpClient();
            _configuration = configuration;
        }

        // GET: api/MidYearPlans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MidYearPlan>>> GetMidYearPlans()
        {
            var midYearPlans = await repository.GetAll();
            if (midYearPlans == null)
            {
                return NotFound();
            }
            return Ok(midYearPlans);
        }

        // GET: api/MidYearPlans/5
        [HttpGet("{companyId}/{id}")]
        //[HttpGet("{id}")]
        public async Task<ActionResult<MidYearPlan>> GetMidYearPlan(int companyId, int id)
        {
            var midYearPlan = await repository.GetById(id);
            if (midYearPlan == null)
            {
                return NotFound();
            }
            var baseURL = _configuration.GetValue<string>("BaseURL");
            var implementationPlansResponse = await _httpClient.GetAsync($"{baseURL}/MidYearPlans/GetImplementationPlanTypes/ByCompanyId/{companyId}");
            if (implementationPlansResponse.IsSuccessStatusCode)
            {
                string apiResponseX = await implementationPlansResponse.Content.ReadAsStringAsync();

                // Split the string into a list of strings based on the delimiter ";"
                List<string> implementationPlans = apiResponseX.Split(';').ToList();

                midYearPlan.AvailablePlans = implementationPlans;
            }
            if (midYearPlan.Plan != null)
            {
                string[] selectedPlans = midYearPlan.Plan.Split(','); // Splitting into an array of strings directly
                midYearPlan.SelectedPlans = selectedPlans.ToList(); // Converting the array to a list of strings
                List<string> availablePlans = new List<string>();
                foreach (string plan in midYearPlan.AvailablePlans)
                {
                    if (!midYearPlan.SelectedPlans.Contains(plan))
                    {
                        availablePlans.Add(plan);
                    }
                }
                midYearPlan.AvailablePlans = availablePlans;
            }

            return Ok(midYearPlan);
        }

        //[HttpGet("{id}")]
        [HttpGet("ByEmployeeBenefitsPlanId/{companyId}/{planId}")]
        public async Task<ActionResult<MidYearPlan>> GetEmployeeBenefitsPlanMidYearPlans(int companyId, int planId)
        {
            var midYearPlans = await repository.GetAllByCompanyId(entity => entity.EmployeeBenefitsPlanId == planId);
            if (midYearPlans == null)
            {
                return NotFound();
            }
            foreach (var midYearPlan in midYearPlans)
            {
                var baseURL = _configuration.GetValue<string>("BaseURL");
                var implementationPlansResponse = await _httpClient.GetAsync($"{baseURL}/MidYearPlans/GetImplementationPlanTypes/ByCompanyId/{companyId}");
                if (implementationPlansResponse.IsSuccessStatusCode)
                {
                    string apiResponseX = await implementationPlansResponse.Content.ReadAsStringAsync();

                    // Split the string into a list of strings based on the delimiter ";"
                    List<string> implementationPlans = apiResponseX.Split(new char[] { ';', ',' }).ToList();

                    midYearPlan.AvailablePlans = implementationPlans;
                }
                if (midYearPlan.Plan != null)
                {
                    string[] selectedPlans = midYearPlan.Plan.Split(new char[] { ';', ',' }); // Splitting into an array of strings directly
                    midYearPlan.SelectedPlans = selectedPlans.ToList(); // Converting the array to a list of strings
                    midYearPlan.AvailablePlans.RemoveAll(plan => midYearPlan.SelectedPlans.Contains(plan));
                }
                else
                {
                    midYearPlan.SelectedPlans = new List<string>();
                }
            }
            return Ok(midYearPlans);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMidYearPlan(int id, MidYearPlan midYearPlan)
        {
            if (id != midYearPlan.Id)
            {
                return BadRequest();
            }

            var updatedMidYearPlan = await repository.Update(midYearPlan, id);
            if (updatedMidYearPlan == null)
            {
                return NotFound();
            }
            return Ok(updatedMidYearPlan);
        }

        [HttpPost]
        public async Task<ActionResult<MidYearPlan>> PostMidYearPlan(MidYearPlan midYearPlan)
        {
            var addedMidYearPlan = await repository.Add(midYearPlan);
            if (addedMidYearPlan == null)
            {
                return NotFound();
            }
            var existingEBP = await ebpRepo.GetById(midYearPlan.EmployeeBenefitsPlanId);
            existingEBP.IsMidYearPlan = "true";
            await ebpRepo.Update(existingEBP, midYearPlan.EmployeeBenefitsPlanId);
            return Ok(addedMidYearPlan);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMidYearPlan(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }

      

        // DELETE: api/ByEmpl/5
        [HttpDelete("ByEmployeeBenefitsPlanId/{id}")]
        public async Task<IActionResult> DeleteAllByPlanId(int id)
        {
            var deletedResponse = await repository.DeleteByCompanyId(entity => entity.EmployeeBenefitsPlanId == id);
            var existingEBP = await ebpRepo.GetById(id);
            existingEBP.IsMidYearPlan = "false";
            await ebpRepo.Update(existingEBP, id);
            return Ok(deletedResponse);
        }

        [HttpGet("GetImplementationPlanTypes/ByCompanyId/{id}")]
        public async Task<ActionResult<string>> GetImplementationPlanTypes(int id)
        {
            var companyDetails = await companyDetailsRepository.GetByCompanyId(entity => entity.CompanyId == id);
            if (companyDetails == null)
            {
                return NotFound();
            }
            string updatedString = companyDetails.ImplementationPlanType.Replace(";COBRA", string.Empty).Replace("COBRA;", string.Empty);
            companyDetails.ImplementationPlanType = updatedString.Trim(';');
            return Ok(companyDetails.ImplementationPlanType);
        }

        [HttpGet("ByParentPlanId/{companyId}/{planId}")]
        public async Task<ActionResult<string>> GetSelectedEmployeeBenefitsPlanTypes(int companyId, int planId)
        {
            var midYearPlans = await repository.GetAllByCompanyId(entity => entity.EmployeeBenefitsPlanId == planId);

            var companyDetails = await companyDetailsRepository.GetByCompanyId(entity => entity.CompanyId == companyId);
            if (companyDetails != null && midYearPlans != null && midYearPlans.All(pt => pt.Plan != null))
            {
                var selectedPlans = midYearPlans.Select(p => p.Plan).SelectMany(r => r.Split(new char[] { ';', ',' })).Distinct();
                string updatedString = companyDetails.ImplementationPlanType.Replace(";COBRA", string.Empty).Replace("COBRA;", string.Empty);
                companyDetails.ImplementationPlanType = updatedString.Trim(';');
                var plans = companyDetails.ImplementationPlanType.Split(new char[] { ';', ',' });
                var availablePlans = plans.Except(selectedPlans).ToList();
                return Ok(string.Join(";", availablePlans));


            }

            if (midYearPlans.All(pt => pt.Plan == null) || midYearPlans.All(pt => pt.Plan == ""))
            {
                string updatedString = companyDetails.ImplementationPlanType.Replace(";COBRA", string.Empty).Replace("COBRA;", string.Empty);
                companyDetails.ImplementationPlanType = updatedString.Trim(';');
                var initialPlans = companyDetails.ImplementationPlanType;
                return Ok(initialPlans);
            }

            return NotFound();
        }

    }
}
