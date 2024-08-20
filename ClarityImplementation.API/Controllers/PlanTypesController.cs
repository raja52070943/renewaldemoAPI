using Azure;
using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models;
using ClarityImplementation.API.Models.Plans;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.Design;
using System.Configuration;
using System.Numerics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanTypesController : Controller
    {
        private readonly GenericRepository<PlanType> repository;
        private readonly GenericRepository<EmployeeBenefitsPlan> ebpRepo;
        private readonly GenericRepository<CompanyDetails> companyDetailsRepository;

        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public PlanTypesController(ClarityDbContext context, IConfiguration configuration)
        {
            repository = new GenericRepository<PlanType>(context);
            ebpRepo = new GenericRepository<EmployeeBenefitsPlan>(context);
            companyDetailsRepository = new GenericRepository<CompanyDetails>(context);

            _httpClient = new HttpClient();
            _configuration = configuration;
        }

        // GET: api/PlanTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlanType>>> GetPlanTypes()
        {
            var planTypes = await repository.GetAll();
            if (planTypes == null)
            {
                return NotFound();
            }
            return Ok(planTypes);
        }

        // GET: api/PlanTypes/5
        [HttpGet("{companyId}/{id}")]
        public async Task<ActionResult<PlanType>> GetPlanType(int companyId, int id)
        {
            var planType = await repository.GetById(id);
            if (planType == null)
            {
                return NotFound();
            }

            if (planType.Group != null)
            {
                string[] selectedGroups = planType.Group.Split(','); // Splitting into an array of strings directly
                planType.SelectedGroups = selectedGroups.ToList(); // Converting the array to a list of strings
            }

            var baseURL = _configuration.GetValue<string>("BaseURL");
            var implementationPlansResponse = await _httpClient.GetAsync($"{baseURL}/PlanTypes/GetImplementationPlanTypes/ByCompanyId/{companyId}");
            if (implementationPlansResponse.IsSuccessStatusCode)
            {
                string apiResponseX = await implementationPlansResponse.Content.ReadAsStringAsync();
                string updatedString = apiResponseX.Replace(";COBRA", string.Empty).Replace("COBRA;", string.Empty);
                apiResponseX = updatedString.Trim(';');
                // Split the string into a list of strings based on the delimiter ";"
                List<string> implementationPlans = apiResponseX.Split(';').ToList();

                planType.AvailablePlans = implementationPlans;
            }
            if (planType.Plan != null)
            {
                string[] selectedPlans = planType.Plan.Split(','); // Splitting into an array of strings directly
                planType.SelectedPlans = selectedPlans.ToList(); // Converting the array to a list of strings
                List<string> availablePlans = new List<string>();
                foreach (string plan in planType.AvailablePlans)
                {
                    if (!planType.SelectedPlans.Contains(plan))
                    {
                        availablePlans.Add(plan);
                    }
                }
                planType.AvailablePlans = availablePlans;
            }

            return Ok(planType);
        }

        [HttpGet("ByEmployeeBenefitsPlanId/{companyId}/{planId}")]
        public async Task<ActionResult<PlanType>> GetEmployeeBenefitsPlanTypes(int companyId, int planId)
        {
            var planTypes = await repository.GetAllByCompanyId(entity => entity.EmployeeBenefitsPlanId == planId);
            if (planTypes == null)
            {
                return NotFound();
            }

            foreach (var planType in planTypes)
            {
                if (planType.Group != null)
                {
                    string[] selectedGroups = planType.Group.Split(new char[] { ';', ',' }); // Splitting into an array of strings directly
                    planType.SelectedGroups = selectedGroups.ToList(); // Converting the array to a list of strings
                }

                var baseURL = _configuration.GetValue<string>("BaseURL");
                var implementationPlansResponse = await _httpClient.GetAsync($"{baseURL}/PlanTypes/GetImplementationPlanTypes/ByCompanyId/{companyId}");
                if (implementationPlansResponse.IsSuccessStatusCode)
                {
                    string apiResponseX = await implementationPlansResponse.Content.ReadAsStringAsync();
                    string updatedString = apiResponseX.Replace(";COBRA", string.Empty).Replace("COBRA;", string.Empty);
                    apiResponseX = updatedString.Trim(';');
                    // Split the string into a list of strings based on the delimiter ";"
                    List<string> implementationPlans = apiResponseX.Split(new char[] { ';', ',' }).ToList();

                    planType.AvailablePlans = implementationPlans;
                }
                if (planType.Plan != null)
                {
                    string[] selectedPlans = planType.Plan.Split(new char[] { ';', ',' }); // Splitting into an array of strings directly
                    planType.SelectedPlans = selectedPlans.ToList(); // Converting the array to a list of strings
                    planType.AvailablePlans.RemoveAll(plan => planType.SelectedPlans.Contains(plan));
                }
                else
                {
                    planType.SelectedPlans = new List<string>();
                }

            }

            return Ok(planTypes);
        }






        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlanType(int id, PlanType planType)
        {
            if (id != planType.Id)
            {
                return BadRequest();
            }

            var updatedPlanType = await repository.Update(planType, id);
            if (updatedPlanType == null)
            {
                return NotFound();
            }
            return Ok(updatedPlanType);
        }

        [HttpPost]
        public async Task<ActionResult<PlanType>> PostPlanType(PlanType planType)
        {
            var addedPlanType = await repository.Add(planType);
            if (addedPlanType == null)
            {
                return NotFound();
            }
            var existingEBP = await ebpRepo.GetById(planType.EmployeeBenefitsPlanId);
           
            await ebpRepo.Update(existingEBP, planType.EmployeeBenefitsPlanId);
            return Ok(addedPlanType);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlanType(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
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
            var planTypes = await repository.GetAllByCompanyId(entity => entity.EmployeeBenefitsPlanId == planId);



            var companyDetails = await companyDetailsRepository.GetByCompanyId(entity => entity.CompanyId == companyId);
            if (companyDetails != null && planTypes != null && planTypes.All(pt => pt.Plan != null))
            {
                var selectedPlans = planTypes.Select(p => p.Plan).SelectMany(r => r.Split(new char[] { ';', ',' })).Distinct();
                string updatedString = companyDetails.ImplementationPlanType.Replace(";COBRA", string.Empty).Replace("COBRA;", string.Empty);
                companyDetails.ImplementationPlanType = updatedString.Trim(';');
                var plans = companyDetails.ImplementationPlanType.Split(new char[] { ';', ',' });
                var availablePlans = plans.Except(selectedPlans).ToList();
                return Ok(string.Join(";", availablePlans));

            }

            if (planTypes.All(pt => pt.Plan == null) || planTypes.All(pt => pt.Plan == ""))
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
