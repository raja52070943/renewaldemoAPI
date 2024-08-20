using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models;
using ClarityImplementation.API.Models.Plans;
using ClarityImplementation.API.Models.Plans.COBRA;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class COBRAPlansController : ControllerBase
    {
        private readonly GenericRepository<COBRAPlan> repository;
        private readonly GenericRepository<CobraGeneralInformation> cobraGeneralInformationRepository;
        private readonly GenericRepository<CobraOpenEnrollmentManagement> cobraOpenEnrollmentManagementRepository;

        private readonly GenericRepository<EnrollmentAndEligibilityContact> enrollmentAndEligibilityContactRepository;
        private readonly GenericRepository<COBRAMedicalPlan> cobraMedicalPlanRepository;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public COBRAPlansController(ClarityDbContext context, IConfiguration configuration)
        {
            repository = new GenericRepository<COBRAPlan>(context);
            enrollmentAndEligibilityContactRepository = new GenericRepository<EnrollmentAndEligibilityContact>(context);
            cobraGeneralInformationRepository = new GenericRepository<CobraGeneralInformation>(context);
            cobraOpenEnrollmentManagementRepository = new GenericRepository<CobraOpenEnrollmentManagement>(context);
            cobraMedicalPlanRepository = new GenericRepository<COBRAMedicalPlan>(context);

            _httpClient = new HttpClient();
            _configuration = configuration;
        }

        // GET: api/COBRAPlans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<COBRAPlan>>> GetCOBRAPlans()
        {
            var COBRAPlans = await repository.GetAll();
            if (COBRAPlans == null)
            {
                return NotFound();
            }
            return Ok(COBRAPlans);
        }

        [HttpGet("ByCompanyId/{id}")]
        public async Task<ActionResult<COBRAPlan>> GetCompanyCOBRAPlan(int id)
        {
            var COBRAPlan = await repository.GetByCompanyId(entity => entity.CompanyId == id);
            if (COBRAPlan == null)
            {
                return NotFound();
            }
            return Ok(COBRAPlan);
        }


        [HttpGet("{id}")]
        //[HttpGet("{id}")]
        public async Task<ActionResult<COBRAPlan>> GetCOBRAPlan(int id)
        {
            var baseURL = _configuration.GetValue<string>("BaseURL");
            var cobraPlan = await repository.GetById(id);
            if (cobraPlan == null)
            {
                return NotFound();
            }
            var cobraGeneralInformationResponse = await _httpClient.GetAsync(baseURL + "/CobraGeneralInformations/ByPlanId/" + id);
            if (cobraGeneralInformationResponse.IsSuccessStatusCode)
            {
                string apiResponse = await cobraGeneralInformationResponse.Content.ReadAsStringAsync();
                var cobraGeneralInfo = JsonConvert.DeserializeObject<CobraGeneralInformation>(apiResponse);
                cobraPlan.CobraGeneralInformation = cobraGeneralInfo;
            }


            var COBRAMedicalPlanResponse = await _httpClient.GetAsync(baseURL + "/COBRAMedicalPlans/ByPlanId/" + id);
            if (COBRAMedicalPlanResponse.IsSuccessStatusCode)
            {
                string apiResponse = await COBRAMedicalPlanResponse.Content.ReadAsStringAsync();
                //var address = JsonConvert.DeserializeObject<List<Address>>(apiResponse);
                var plans = JsonConvert.DeserializeObject<List<COBRAMedicalPlan>>(apiResponse);
                cobraPlan.COBRAMedicalPlans = plans;
            }


            var COBRADentalPlanResponse = await _httpClient.GetAsync(baseURL + "/COBRADentalPlans/ByPlanId/" + id);
            if (COBRADentalPlanResponse.IsSuccessStatusCode)
            {
                string apiResponse = await COBRADentalPlanResponse.Content.ReadAsStringAsync();
                //var address = JsonConvert.DeserializeObject<List<Address>>(apiResponse);
                var plans = JsonConvert.DeserializeObject<List<COBRADentalPlan>>(apiResponse);
                cobraPlan.COBRADentalPlans = plans;
            }

            var COBRAVisionPlanResponse = await _httpClient.GetAsync(baseURL + "/COBRAVisionPlans/ByPlanId/" + id);
            if (COBRAVisionPlanResponse.IsSuccessStatusCode)
            {
                string apiResponse = await COBRAVisionPlanResponse.Content.ReadAsStringAsync();
                //var address = JsonConvert.DeserializeObject<List<Address>>(apiResponse);
                var plans = JsonConvert.DeserializeObject<List<COBRAVisionPlan>>(apiResponse);
                cobraPlan.COBRAVisionPlans = plans;
            }

            var COBRAHRAPlanResponse = await _httpClient.GetAsync(baseURL + "/COBRAHRAPlans/ByPlanId/" + id);
            if (COBRAHRAPlanResponse.IsSuccessStatusCode)
            {
                string apiResponse = await COBRAHRAPlanResponse.Content.ReadAsStringAsync();
                //var address = JsonConvert.DeserializeObject<List<Address>>(apiResponse);
                var plans = JsonConvert.DeserializeObject<List<COBRAHRAPlan>>(apiResponse);
                cobraPlan.COBRAHRAPlans = plans;
            }

            var COBRAEAPPlanResponse = await _httpClient.GetAsync(baseURL + "/COBRAEAPPlans/ByPlanId/" + id);
            if (COBRAEAPPlanResponse.IsSuccessStatusCode)
            {
                string apiResponse = await COBRAEAPPlanResponse.Content.ReadAsStringAsync();
                //var address = JsonConvert.DeserializeObject<List<Address>>(apiResponse);
                var plans = JsonConvert.DeserializeObject<List<COBRAEAPPlan>>(apiResponse);
                cobraPlan.COBRAEAPPlans = plans;
            }

            var COBRAFSAPlanResponse = await _httpClient.GetAsync(baseURL + "/COBRAFSAPlans/ByPlanId/" + id);
            if (COBRAFSAPlanResponse.IsSuccessStatusCode)
            {
                string apiResponse = await COBRAFSAPlanResponse.Content.ReadAsStringAsync();
                //var address = JsonConvert.DeserializeObject<List<Address>>(apiResponse);
                var plans = JsonConvert.DeserializeObject<List<COBRAFSAPlan>>(apiResponse);
                cobraPlan.COBRAFSAPlans = plans;
            }

            var COBRAInsurancePlanResponse = await _httpClient.GetAsync(baseURL + "/COBRAInsurancePlans/ByPlanId/" + id);
            if (COBRAInsurancePlanResponse.IsSuccessStatusCode)
            {
                string apiResponse = await COBRAInsurancePlanResponse.Content.ReadAsStringAsync();
                //var address = JsonConvert.DeserializeObject<List<Address>>(apiResponse);
                var plans = JsonConvert.DeserializeObject<List<COBRAInsurancePlan>>(apiResponse);
                cobraPlan.COBRAInsurancePlans = plans;
            }

            var enrollmentAndEligibilityContactResponse = await _httpClient.GetAsync(baseURL + "/EnrollmentAndEligibilityContacts/ByPlanId/" + id);
            if (enrollmentAndEligibilityContactResponse.IsSuccessStatusCode)
            {
                string apiResponseEEC = await enrollmentAndEligibilityContactResponse.Content.ReadAsStringAsync();
                var enrollmentAndEligibilityContacts = JsonConvert.DeserializeObject<List<EnrollmentAndEligibilityContact>>(apiResponseEEC);
                cobraPlan.EnrollmentAndEligibilityContact = enrollmentAndEligibilityContacts;
            }

            var cobraOpenEnrollmentManagementResponse = await _httpClient.GetAsync(baseURL + "/CobraOpenEnrollmentManagement/ByPlanId/" + id);
            if (cobraOpenEnrollmentManagementResponse.IsSuccessStatusCode)
            {
                string apiResponseOpenEnrollment = await cobraOpenEnrollmentManagementResponse.Content.ReadAsStringAsync();
                var openEnrollment = JsonConvert.DeserializeObject<CobraOpenEnrollmentManagement>(apiResponseOpenEnrollment);
                cobraPlan.CobraOpenEnrollmentManagement = openEnrollment;
            }

            return Ok(cobraPlan);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCOBRAPlan(int id, COBRAPlan COBRAPlan)
        {
            if (id != COBRAPlan.Id)
            {
                return BadRequest();
            }

            var updatedCOBRAPlan = await repository.Update(COBRAPlan, id);
            if (updatedCOBRAPlan == null)
            {
                return NotFound();
            }
            return Ok(updatedCOBRAPlan);
        }

        [HttpPost]
        public async Task<ActionResult<IActionResult>> PostCOBRAPlan(COBRAPlan COBRAPlan)
        {

            var addedCOBRAPlan = await repository.Add(COBRAPlan);
            if (addedCOBRAPlan == null)
            {
                return NotFound();
            }

            EnrollmentAndEligibilityContact enrollmentAndEligibilityContact = new EnrollmentAndEligibilityContact();
            enrollmentAndEligibilityContact.COBRAPlanId = addedCOBRAPlan.Id;
            await enrollmentAndEligibilityContactRepository.Add(enrollmentAndEligibilityContact);

            CobraOpenEnrollmentManagement cobraOpenEnrollmentManagement = new CobraOpenEnrollmentManagement();
            cobraOpenEnrollmentManagement.COBRAPlanId = addedCOBRAPlan.Id;
            await cobraOpenEnrollmentManagementRepository.Add(cobraOpenEnrollmentManagement);


            return RedirectToAction("GetCOBRAPlan", new { id = addedCOBRAPlan.Id });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCOBRAPlan(int id)
        {
            var response = await repository.Delete(id);
            return Ok(response);
        }

        [HttpGet("COBRAProgress/{id}")]
        public async Task<ActionResult<CaseProgress>> GetCOBRAProgress(int id)
        {
            var cobraPlan = await repository.GetById(id);
            if (cobraPlan == null)
            {
                return NotFound();
            }
            return Ok(new CaseProgress { Progress=cobraPlan.Progress});
        }



    }
}
