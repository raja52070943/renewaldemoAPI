using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models;
using ClarityImplementation.API.Models.Plans;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Configuration;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeBenefitsPlansController : ControllerBase
    {
        private readonly GenericRepository<EmployeeBenefitsPlan> repository;
        private readonly GenericRepository<EmployeeBenefitsGeneralInformation> generalRepository;
        private readonly GenericRepository<MidYearPlan> midRepo;
        private readonly GenericRepository<PlanType> planRepo;
        private readonly GenericRepository<PriorYearPlan> priorRepo;
        private readonly GenericRepository<EmployeeBenefitsEnrollment> enrollRepo;
        private readonly GenericRepository<EmployeeBenefitsFSA> fsaRepo;
        private readonly GenericRepository<EmployeeBenefitsHSA> hsaRepo;
        private readonly GenericRepository<EmployeeBenefitsHRA> hraRepo;
        private readonly GenericRepository<EmployeeContributionGroup> ecgRepo;
        private readonly GenericRepository<PayScheduleType> payScheduleRepo;
        private readonly GenericRepository<EmployeeBenefitsSmartRide> srRepo;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public EmployeeBenefitsPlansController(ClarityDbContext context, IConfiguration configuration)
        {
            repository = new GenericRepository<EmployeeBenefitsPlan>(context);
            midRepo = new GenericRepository<MidYearPlan>(context);
            generalRepository = new GenericRepository<EmployeeBenefitsGeneralInformation>(context);
            priorRepo = new GenericRepository<PriorYearPlan>(context);
            enrollRepo = new GenericRepository<EmployeeBenefitsEnrollment>(context);
            fsaRepo = new GenericRepository<EmployeeBenefitsFSA>(context);
            hsaRepo = new GenericRepository<EmployeeBenefitsHSA>(context);
            hraRepo = new GenericRepository<EmployeeBenefitsHRA>(context);
            ecgRepo = new GenericRepository<EmployeeContributionGroup>(context);
            planRepo = new GenericRepository<PlanType>(context);
            srRepo = new GenericRepository<EmployeeBenefitsSmartRide>(context);
            payScheduleRepo = new GenericRepository<PayScheduleType>(context);
            _httpClient = new HttpClient();
            _configuration = configuration;
        }

        // GET: api/EmployeeBenefitsPlans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeBenefitsPlan>>> GetEmployeeBenefitsPlans()
        {
            var employeeBenefitsPlans = await repository.GetAll();
            if (employeeBenefitsPlans == null)
            {
                return NotFound();
            }
            return Ok(employeeBenefitsPlans);
        }

        [HttpGet("ByCompanyId/{id}")]
        public async Task<ActionResult<EmployeeBenefitsPlan>> GetCompanyEmployeeBenefitsPlan(int id)
        {
            var baseURL = _configuration.GetValue<string>("BaseURL");
            var employeeBenefitsPlan = await repository.GetByCompanyId(entity => entity.CompanyId == id);
            if (employeeBenefitsPlan == null)
            {
                return NotFound();
            }

            var generalInfoResponse = await _httpClient.GetAsync(baseURL + "/EmployeeBenefitsGeneralInformations/ByEmployeeBenefitsPlanId/" + employeeBenefitsPlan.Id);
            if (generalInfoResponse.IsSuccessStatusCode)
            {
                string generalInfoAPIResponse = await generalInfoResponse.Content.ReadAsStringAsync();
                var employeeBenefitsGeneralInformation = JsonConvert.DeserializeObject<EmployeeBenefitsGeneralInformation>(generalInfoAPIResponse);
                if (employeeBenefitsGeneralInformation != null)
                {
                    employeeBenefitsPlan.EmployeeBenefitsGeneralInformation = employeeBenefitsGeneralInformation;
                }
            }

            var response1 = await _httpClient.GetAsync(baseURL + "/PlanTypes/ByEmployeeBenefitsPlanId/" + id + "/" + employeeBenefitsPlan.Id);
            if (response1.IsSuccessStatusCode)
            {
                string apiResponse1 = await response1.Content.ReadAsStringAsync();
                //var address = JsonConvert.DeserializeObject<List<Address>>(apiResponse);
                var employeeBenefitsPlanTypes = JsonConvert.DeserializeObject<List<PlanType>>(apiResponse1);
                employeeBenefitsPlan.PlanTypes = employeeBenefitsPlanTypes;
            }

            var responseZ = await _httpClient.GetAsync(baseURL + "/PayScheduleTypes/ByEmployeeBenefitsPlanId/" + employeeBenefitsPlan.Id);
            if (responseZ.IsSuccessStatusCode)
            {
                string apiResponseZ = await responseZ.Content.ReadAsStringAsync();
                var employeeBenefitsPayScheduleTypes = JsonConvert.DeserializeObject<List<PayScheduleType>>(apiResponseZ);
                employeeBenefitsPlan.PayScheduleTypes = employeeBenefitsPayScheduleTypes;
            }


            var hraResponse = await _httpClient.GetAsync(baseURL + "/EmployeeBenefitsHRAs/ByEmployeeBenefitsPlanId/" + employeeBenefitsPlan.Id);
            if (hraResponse.IsSuccessStatusCode)
            {
                string hraAPIResponse = await hraResponse.Content.ReadAsStringAsync();
                //var address = JsonConvert.DeserializeObject<List<Address>>(apiResponse);
                var employeeBenefitsHRA = JsonConvert.DeserializeObject<List<EmployeeBenefitsHRA>>(hraAPIResponse);
                if (employeeBenefitsHRA != null)
                {
                    employeeBenefitsPlan.EmployeeBenefitsHRAs = employeeBenefitsHRA;
                }
            }

            if (employeeBenefitsPlan.IsMidYearPlan == "true")
            {
                var midYearPlansResponse = await _httpClient.GetAsync(baseURL + "/MidYearPlans/ByEmployeeBenefitsPlanId/" + id + "/" + employeeBenefitsPlan.Id);
                if (midYearPlansResponse.IsSuccessStatusCode)
                {
                    string apiResponse = await midYearPlansResponse.Content.ReadAsStringAsync();
                    //var address = JsonConvert.DeserializeObject<List<Address>>(apiResponse);
                    var employeeBenefitsPlanMidYearPlans = JsonConvert.DeserializeObject<List<MidYearPlan>>(apiResponse);
                    employeeBenefitsPlan.MidYearPlans = employeeBenefitsPlanMidYearPlans;
                }
            }


            if (employeeBenefitsPlan.IsPriorYearPlan == "true")
            {
                var priorYearPlansResponse = await _httpClient.GetAsync(baseURL + "/PriorYearPlans/ByEmployeeBenefitsPlanId/" + id + "/" + employeeBenefitsPlan.Id);
                if (priorYearPlansResponse.IsSuccessStatusCode)
                {
                    string apiPriorResponse = await priorYearPlansResponse.Content.ReadAsStringAsync();
                    //var address = JsonConvert.DeserializeObject<List<Address>>(apiResponse);
                    var employeeBenefitsPlanPriorYearPlans = JsonConvert.DeserializeObject<List<PriorYearPlan>>(apiPriorResponse);
                    employeeBenefitsPlan.PriorYearPlans = employeeBenefitsPlanPriorYearPlans;
                }
            }



            var enrollmentResponse = await _httpClient.GetAsync(baseURL + "/EmployeeBenefitsEnrollments/ByEmployeeBenefitsPlanId/" + employeeBenefitsPlan.Id);
            if (enrollmentResponse.IsSuccessStatusCode)
            {
                string apiResponse2 = await enrollmentResponse.Content.ReadAsStringAsync();
                //var address = JsonConvert.DeserializeObject<List<Address>>(apiResponse);
                var employeeBenefitsEnrollment = JsonConvert.DeserializeObject<EmployeeBenefitsEnrollment>(apiResponse2);
                if (employeeBenefitsEnrollment != null)
                {
                    employeeBenefitsPlan.EmployeeBenefitsEnrollment = employeeBenefitsEnrollment;
                }

            }


            var fsaResponse = await _httpClient.GetAsync(baseURL + "/EmployeeBenefitsFSAs/ByEmployeeBenefitsPlanId/" + employeeBenefitsPlan.Id);
            if (fsaResponse.IsSuccessStatusCode)
            {
                string apiResponse3 = await fsaResponse.Content.ReadAsStringAsync();
                //var address = JsonConvert.DeserializeObject<List<Address>>(apiResponse);
                var employeeBenefitsFSA = JsonConvert.DeserializeObject<EmployeeBenefitsFSA>(apiResponse3);
                if (employeeBenefitsFSA != null)
                {
                    employeeBenefitsPlan.EmployeeBenefitsFSA = employeeBenefitsFSA;

                }
            }


            var hsaResponse = await _httpClient.GetAsync(baseURL + "/EmployeeBenefitsHSAs/ByEmployeeBenefitsPlanId/" + employeeBenefitsPlan.Id);
            if (hsaResponse.IsSuccessStatusCode)
            {
                string hsaAPIResponse = await hsaResponse.Content.ReadAsStringAsync();
                //var address = JsonConvert.DeserializeObject<List<Address>>(apiResponse);
                var employeeBenefitsHSA = JsonConvert.DeserializeObject<EmployeeBenefitsHSA>(hsaAPIResponse);
                if (employeeBenefitsHSA != null)
                {
                    employeeBenefitsPlan.EmployeeBenefitsHSA = employeeBenefitsHSA;
                }

            }

            var smartRideResponse = await _httpClient.GetAsync(baseURL + "/EmployeeBenefitsSmartRides/ByEmployeeBenefitsPlanId/" + employeeBenefitsPlan.Id);
            if (smartRideResponse.IsSuccessStatusCode)
            {
                string srAPIResponse = await smartRideResponse.Content.ReadAsStringAsync();
                //var address = JsonConvert.DeserializeObject<List<Address>>(apiResponse);
                var employeeBenefitsSmartRide = JsonConvert.DeserializeObject<EmployeeBenefitsSmartRide>(srAPIResponse);
                if (employeeBenefitsSmartRide != null)
                {
                    employeeBenefitsPlan.EmployeeBenefitsSmartRide = employeeBenefitsSmartRide;
                }

            }
            return Ok(employeeBenefitsPlan);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeBenefitsPlan>> GetEmployeeBenefitsPlan(int id)
        {
            var baseURL = _configuration.GetValue<string>("BaseURL");

            var employeeBenefitsPlan = await repository.GetById(id);
            if (employeeBenefitsPlan == null)
            {
                return NotFound();
            }

            var generalInfoResponse = await _httpClient.GetAsync(baseURL + "/EmployeeBenefitsGeneralInformations/ByEmployeeBenefitsPlanId/" + id);
            if (generalInfoResponse.IsSuccessStatusCode)
            {
                string generalInfoAPIResponse = await generalInfoResponse.Content.ReadAsStringAsync();
                var employeeBenefitsGeneralInformation = JsonConvert.DeserializeObject<EmployeeBenefitsGeneralInformation>(generalInfoAPIResponse);
                if (employeeBenefitsGeneralInformation != null)
                {
                    employeeBenefitsPlan.EmployeeBenefitsGeneralInformation = employeeBenefitsGeneralInformation;
                }
            }

            var response1 = await _httpClient.GetAsync(baseURL + "/PlanTypes/ByEmployeeBenefitsPlanId/" + employeeBenefitsPlan.CompanyId + "/" + id);
            if (response1.IsSuccessStatusCode)
            {
                string apiResponse1 = await response1.Content.ReadAsStringAsync();
                //var address = JsonConvert.DeserializeObject<List<Address>>(apiResponse);
                var employeeBenefitsPlanTypes = JsonConvert.DeserializeObject<List<PlanType>>(apiResponse1);
                employeeBenefitsPlan.PlanTypes = employeeBenefitsPlanTypes;
            }

            var responseZ = await _httpClient.GetAsync(baseURL + "/PayScheduleTypes/ByEmployeeBenefitsPlanId/" + id);
            if (responseZ.IsSuccessStatusCode)
            {
                string apiResponseZ = await responseZ.Content.ReadAsStringAsync();
                var employeeBenefitsPayScheduleTypes = JsonConvert.DeserializeObject<List<PayScheduleType>>(apiResponseZ);
                employeeBenefitsPlan.PayScheduleTypes = employeeBenefitsPayScheduleTypes;
            }


            var hraResponse = await _httpClient.GetAsync(baseURL + "/EmployeeBenefitsHRAs/ByEmployeeBenefitsPlanId/" + id);
            if (hraResponse.IsSuccessStatusCode)
            {
                string hraAPIResponse = await hraResponse.Content.ReadAsStringAsync();
                //var address = JsonConvert.DeserializeObject<List<Address>>(apiResponse);
                var employeeBenefitsHRA = JsonConvert.DeserializeObject<List<EmployeeBenefitsHRA>>(hraAPIResponse);
                if (employeeBenefitsHRA != null)
                {
                    employeeBenefitsPlan.EmployeeBenefitsHRAs = employeeBenefitsHRA;
                }
            }
           
            if (employeeBenefitsPlan.IsMidYearPlan == "true")
            {
                var midYearPlansResponse = await _httpClient.GetAsync(baseURL + "/MidYearPlans/ByEmployeeBenefitsPlanId/" + employeeBenefitsPlan.CompanyId + "/" + id);
                if (midYearPlansResponse.IsSuccessStatusCode)
                {
                    string apiResponse = await midYearPlansResponse.Content.ReadAsStringAsync();
                    //var address = JsonConvert.DeserializeObject<List<Address>>(apiResponse);
                    var employeeBenefitsPlanMidYearPlans = JsonConvert.DeserializeObject<List<MidYearPlan>>(apiResponse);
                    employeeBenefitsPlan.MidYearPlans = employeeBenefitsPlanMidYearPlans;
                }
            }
            

            if (employeeBenefitsPlan.IsPriorYearPlan == "true")
            {
                var priorYearPlansResponse = await _httpClient.GetAsync(baseURL + "/PriorYearPlans/ByEmployeeBenefitsPlanId/" + employeeBenefitsPlan.CompanyId + "/" + id);
                if (priorYearPlansResponse.IsSuccessStatusCode)
                {
                    string apiPriorResponse = await priorYearPlansResponse.Content.ReadAsStringAsync();
                    //var address = JsonConvert.DeserializeObject<List<Address>>(apiResponse);
                    var employeeBenefitsPlanPriorYearPlans = JsonConvert.DeserializeObject<List<PriorYearPlan>>(apiPriorResponse);
                    employeeBenefitsPlan.PriorYearPlans = employeeBenefitsPlanPriorYearPlans;
                }
            }
            


            var enrollmentResponse = await _httpClient.GetAsync(baseURL + "/EmployeeBenefitsEnrollments/ByEmployeeBenefitsPlanId/" + id);
            if (enrollmentResponse.IsSuccessStatusCode)
            {
                string apiResponse2 = await enrollmentResponse.Content.ReadAsStringAsync();
                //var address = JsonConvert.DeserializeObject<List<Address>>(apiResponse);
                var employeeBenefitsEnrollment = JsonConvert.DeserializeObject<EmployeeBenefitsEnrollment>(apiResponse2);
                if (employeeBenefitsEnrollment != null)
                {
                    employeeBenefitsPlan.EmployeeBenefitsEnrollment = employeeBenefitsEnrollment;
                }
                
            }
           

            var fsaResponse = await _httpClient.GetAsync(baseURL + "/EmployeeBenefitsFSAs/ByEmployeeBenefitsPlanId/" + id);
            if (fsaResponse.IsSuccessStatusCode)
            {
                string apiResponse3 = await fsaResponse.Content.ReadAsStringAsync();
                //var address = JsonConvert.DeserializeObject<List<Address>>(apiResponse);
                var employeeBenefitsFSA = JsonConvert.DeserializeObject<EmployeeBenefitsFSA>(apiResponse3);
                if (employeeBenefitsFSA != null)
                {
                    employeeBenefitsPlan.EmployeeBenefitsFSA = employeeBenefitsFSA;

                }
            }
            

            var hsaResponse = await _httpClient.GetAsync(baseURL + "/EmployeeBenefitsHSAs/ByEmployeeBenefitsPlanId/" + id);
            if (hsaResponse.IsSuccessStatusCode)
            {
                string hsaAPIResponse = await hsaResponse.Content.ReadAsStringAsync();
                //var address = JsonConvert.DeserializeObject<List<Address>>(apiResponse);
                var employeeBenefitsHSA = JsonConvert.DeserializeObject<EmployeeBenefitsHSA>(hsaAPIResponse);
                if (employeeBenefitsHSA != null)
                {
                    employeeBenefitsPlan.EmployeeBenefitsHSA = employeeBenefitsHSA;
                }
                
            }
            
            var smartRideResponse = await _httpClient.GetAsync(baseURL + "/EmployeeBenefitsSmartRides/ByEmployeeBenefitsPlanId/" + id);
            if (smartRideResponse.IsSuccessStatusCode)
            {
                string srAPIResponse = await smartRideResponse.Content.ReadAsStringAsync();
                //var address = JsonConvert.DeserializeObject<List<Address>>(apiResponse);
                var employeeBenefitsSmartRide = JsonConvert.DeserializeObject<EmployeeBenefitsSmartRide>(srAPIResponse);
                if (employeeBenefitsSmartRide != null)
                {
                    employeeBenefitsPlan.EmployeeBenefitsSmartRide = employeeBenefitsSmartRide;
                } 
                
            }
           

            return Ok(employeeBenefitsPlan);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeBenefitsPlan(int id, EmployeeBenefitsPlan employeeBenefitsPlan)
        {
            if (id != employeeBenefitsPlan.Id)
            {
                return BadRequest();
            }

            var updatedEmployeeBenefitsPlan = await repository.Update(employeeBenefitsPlan, id);
            if (updatedEmployeeBenefitsPlan == null)
            {
                return NotFound();
            }
            return Ok(updatedEmployeeBenefitsPlan);
        }

        [HttpPost]
        public async Task<ActionResult<IActionResult>> PostEmployeeBenefitsPlan(EmployeeBenefitsPlan employeeBenefitsPlan)
        {
            var addedEmployeeBenefitsPlan = await repository.Add(employeeBenefitsPlan);
            if (addedEmployeeBenefitsPlan == null)
            {
                return NotFound();
            }

            EmployeeBenefitsGeneralInformation employeeBenefitsGeneralInformation = new EmployeeBenefitsGeneralInformation();
            employeeBenefitsGeneralInformation.EmployeeBenefitsPlanId = employeeBenefitsPlan.Id;
            await generalRepository.Add(employeeBenefitsGeneralInformation);

            PlanType planType = new PlanType();
            planType.EmployeeBenefitsPlanId = addedEmployeeBenefitsPlan.Id;
            await planRepo.Add(planType);

            PayScheduleType payScheduleType = new PayScheduleType();
            planType.EmployeeBenefitsPlanId = addedEmployeeBenefitsPlan.Id;
            await payScheduleRepo.Add(payScheduleType);

            MidYearPlan midYearPlan = new MidYearPlan();
            midYearPlan.EmployeeBenefitsPlanId = addedEmployeeBenefitsPlan.Id;
            await midRepo.Add(midYearPlan);

            EmployeeBenefitsHRA employeeBenefitsHRA = new EmployeeBenefitsHRA();
            employeeBenefitsHRA.EmployeeBenefitsPlanId = addedEmployeeBenefitsPlan.Id;
            await hraRepo.Add(employeeBenefitsHRA);

            PriorYearPlan priorYearPlan = new PriorYearPlan();
            priorYearPlan.EmployeeBenefitsPlanId = addedEmployeeBenefitsPlan.Id;
            await priorRepo.Add(priorYearPlan);

            EmployeeBenefitsEnrollment employeeBenefitsEnrollment = new EmployeeBenefitsEnrollment();
            employeeBenefitsEnrollment.EmployeeBenefitsPlanId = addedEmployeeBenefitsPlan.Id;
            await enrollRepo.Add(employeeBenefitsEnrollment);

            EmployeeBenefitsFSA employeeBenefitsFSA = new EmployeeBenefitsFSA();
            employeeBenefitsFSA.EmployeeBenefitsPlanId = addedEmployeeBenefitsPlan.Id;
            await fsaRepo.Add(employeeBenefitsFSA);

            EmployeeBenefitsHSA employeeBenefitsHSA = new EmployeeBenefitsHSA();
            employeeBenefitsHSA.EmployeeBenefitsPlanId = addedEmployeeBenefitsPlan.Id;
            await hsaRepo.Add(employeeBenefitsHSA);

            EmployeeBenefitsSmartRide employeeBenefitsSmartRide = new EmployeeBenefitsSmartRide();
            employeeBenefitsSmartRide.EmployeeBenefitsPlanId = addedEmployeeBenefitsPlan.Id;
            await srRepo.Add(employeeBenefitsSmartRide);

            return RedirectToAction("GetEmployeeBenefitsPlan", new { id = addedEmployeeBenefitsPlan.Id });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeBenefitsPlan(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }

        [HttpGet("EmployeeBenefitsProgress/{id}")]
        public async Task<ActionResult<CaseProgress>> GetEmployeeBenefitsProgress(int id)
        {
            var employeeBenefitsPlan = await repository.GetById(id);
            if (employeeBenefitsPlan == null)
            {
                return NotFound();
            }
            return Ok(new CaseProgress { Progress = employeeBenefitsPlan.Progress });
        }

    }
}
