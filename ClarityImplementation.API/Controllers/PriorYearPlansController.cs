using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models;
using ClarityImplementation.API.Models.Plans;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.Design;
using System.Configuration;
using System.Drawing.Drawing2D;
using System.Net.Http;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriorYearPlansController : ControllerBase
    {
        private readonly GenericRepository<PriorYearPlan> repository;
        private readonly GenericRepository<EmployeeBenefitsPlan> ebpRepo;
        private readonly GenericRepository<CompanyDetails> companyDetailsRepository;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public PriorYearPlansController(ClarityDbContext context, IConfiguration configuration)
        {
            repository = new GenericRepository<PriorYearPlan>(context);
            ebpRepo = new GenericRepository<EmployeeBenefitsPlan>(context);
            companyDetailsRepository = new GenericRepository<CompanyDetails>(context);

            _httpClient = new HttpClient();
            _configuration = configuration;
        }

        // GET: api/PriorYearPlans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PriorYearPlan>>> GetPriorYearPlans()
        {
            var priorYearPlans = await repository.GetAll();
            if (priorYearPlans == null)
            {
                return NotFound();
            }
            return Ok(priorYearPlans);
        }

        // GET: api/PriorYearPlans/5
        [HttpGet("{companyId}/{id}")]
        public async Task<ActionResult<PriorYearPlan>> GetPriorYearPlan(int companyId, int id)
        {
            var priorYearPlan = await repository.GetById(id);
            if (priorYearPlan == null)
            {
                return NotFound();
            }

            var baseURL = _configuration.GetValue<string>("BaseURL");
            var implementationPlansResponse = await _httpClient.GetAsync($"{baseURL}/PriorYearPlans/GetImplementationPlanTypes/ByCompanyId/{companyId}");
            if (implementationPlansResponse.IsSuccessStatusCode)
            {
                string apiResponseX = await implementationPlansResponse.Content.ReadAsStringAsync();

                // Split the string into a list of strings based on the delimiter ";"
                List<string> implementationPlans = apiResponseX.Split(';').ToList();

                priorYearPlan.AvailablePlans = implementationPlans;
            }
            if (priorYearPlan.Plan != null)
            {
                string[] selectedPlans = priorYearPlan.Plan.Split(','); // Splitting into an array of strings directly
                priorYearPlan.SelectedPlans = selectedPlans.ToList(); // Converting the array to a list of strings
                List<string> availablePlans = new List<string>();
                foreach (string plan in priorYearPlan.AvailablePlans)
                {
                    if (!priorYearPlan.SelectedPlans.Contains(plan))
                    {
                        availablePlans.Add(plan);
                    }
                }
                priorYearPlan.AvailablePlans = availablePlans;
            }

            return Ok(priorYearPlan);
        }

        //[HttpGet("{id}")]
        [HttpGet("ByEmployeeBenefitsPlanId/{companyId}/{planId}")]
        public async Task<ActionResult<PriorYearPlan>> GetEmployeeBenefitsPlanMidYearPlans(int companyId, int planId)
        {
            var priorYearPlans = await repository.GetAllByCompanyId(entity => entity.EmployeeBenefitsPlanId == planId);
            if (priorYearPlans == null)
            {
                return NotFound();
            }
            foreach (var priorYearPlan in priorYearPlans)
            {
                var baseURL = _configuration.GetValue<string>("BaseURL");
                var implementationPlansResponse = await _httpClient.GetAsync($"{baseURL}/PriorYearPlans/GetImplementationPlanTypes/ByCompanyId/{companyId}");
                if (implementationPlansResponse.IsSuccessStatusCode)
                {
                    string apiResponseX = await implementationPlansResponse.Content.ReadAsStringAsync();

                    // Split the string into a list of strings based on the delimiter ";"
                    List<string> implementationPlans = apiResponseX.Split(new char[] { ';', ',' }).ToList();

                    priorYearPlan.AvailablePlans = implementationPlans;
                }
                if (priorYearPlan.Plan != null)
                {
                    string[] selectedPlans = priorYearPlan.Plan.Split(new char[] { ';', ',' }); // Splitting into an array of strings directly
                    priorYearPlan.SelectedPlans = selectedPlans.ToList(); // Converting the array to a list of strings
                    priorYearPlan.AvailablePlans.RemoveAll(plan => priorYearPlan.SelectedPlans.Contains(plan));
                }
                else
                {
                    priorYearPlan.SelectedPlans = new List<string>();
                }
            }
            return Ok(priorYearPlans);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPriorYearPlan(int id, PriorYearPlan priorYearPlan)
        {
            if (id != priorYearPlan.Id)
            {
                return BadRequest();
            }

            var updatedPriorYearPlan = await repository.Update(priorYearPlan, id);
            if (updatedPriorYearPlan == null)
            {
                return NotFound();
            }
            return Ok(updatedPriorYearPlan);
        }

        [HttpPost]
        public async Task<ActionResult<PriorYearPlan>> PostPriorYearPlan(PriorYearPlan priorYearPlan)
        {
            var addedPriorYearPlan = await repository.Add(priorYearPlan);
            if (addedPriorYearPlan == null)
            {
                return NotFound();
            }
            var existingEBP = await ebpRepo.GetById(priorYearPlan.EmployeeBenefitsPlanId);
            existingEBP.IsPriorYearPlan = "true";

            await ebpRepo.Update(existingEBP, priorYearPlan.EmployeeBenefitsPlanId);
            return Ok(addedPriorYearPlan);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePriorYearPlan(int id)
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
            existingEBP.IsPriorYearPlan = "false";
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
            var priorYearPlans = await repository.GetAllByCompanyId(entity => entity.EmployeeBenefitsPlanId == planId);


            var companyDetails = await companyDetailsRepository.GetByCompanyId(entity => entity.CompanyId == companyId);
            if (companyDetails != null && priorYearPlans != null && priorYearPlans.All(pt => pt.Plan != null))
            {
                var selectedPlans = priorYearPlans.Select(p => p.Plan).SelectMany(r => r.Split(new char[] { ';', ',' })).Distinct();
                string updatedString = companyDetails.ImplementationPlanType.Replace(";COBRA", string.Empty).Replace("COBRA;", string.Empty);
                companyDetails.ImplementationPlanType = updatedString.Trim(';');
                var plans = companyDetails.ImplementationPlanType.Split(new char[] { ';', ',' });
                var availablePlans = plans.Except(selectedPlans).ToList();
                return Ok(string.Join(";", availablePlans));

            }

            if (priorYearPlans.All(pt => pt.Plan == null) || priorYearPlans.All(pt => pt.Plan == ""))
            {
                string updatedString = companyDetails.ImplementationPlanType.Replace(";COBRA", string.Empty).Replace("COBRA;", string.Empty);
                companyDetails.ImplementationPlanType = updatedString.Trim(';');
                var initialPlans = companyDetails.ImplementationPlanType;
                return Ok(initialPlans);
            }
            //string.Join(";", selectedPlans)

            return NotFound();
        }

    }
}
