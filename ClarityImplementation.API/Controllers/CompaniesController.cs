using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models;
using ClarityImplementation.API.Models.APIResponse;
using ClarityImplementation.API.Models.FileUpload;
using ClarityImplementation.API.Models.Funding;
using ClarityImplementation.API.Models.Plans;
using ClarityImplementation.API.Models.Plans.COBRA;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly GenericRepository<Company> repository;
        private readonly GenericRepository<CobraGeneralInformation> cobraGeneralInformationRepository;
        private readonly GenericRepository<CompanyDetails> companyDetailsRepository;
        private readonly GenericRepository<EmployeeBenefitsGeneralInformation> generalRepository;
        private readonly GenericRepository<Address> addressRepository;
        private readonly GenericRepository<CompanyContact> companyContactRepository;
        private readonly GenericRepository<COBRAClientContact> cobraClientContactRepository;
        private readonly GenericRepository<COBRABrokerContact> cobraBrokerContactRepository;
        private readonly GenericRepository<BrokerContact> brokerContactRepository;
        private readonly GenericRepository<EnrollmentAndEligibilityContact> enrollmentAndEligibilityContactRepository;
        private readonly GenericRepository<EmployeeBenefitsPlan> employeeBenefitsPlanRepository;
        private readonly GenericRepository<COBRAPlan> cobraPlanRepository;
        private readonly GenericRepository<EmployeeBenefitsFunding> employeeBenefitsFundingRepository;
        private readonly GenericRepository<COBRAFunding> cobraFundingRepository;
        private readonly GenericRepository<EmployeeBenefitsDCA> employeeBenefitsDCARepository;
        private readonly GenericRepository<EmployeeBenefitsLPFSA> employeeBenefitsLPFSARepository;
        private readonly GenericRepository<PlanType> planTypeRepository;
        private readonly GenericRepository<EmployeeBenefitsEnrollment> employeeBenefitsEnrollmentRepository;
        private readonly GenericRepository<EmployeeBenefitsFSA> employeeBenefitsFSARepository;
        private readonly GenericRepository<EmployeeBenefitsHRA> employeeBenefitsHRARepository;
        private readonly GenericRepository<EmployeeBenefitsHSA> employeeBenefitsHSARepository;
        private readonly GenericRepository<EmployeeBenefitsSmartRide> employeeBenefitsSmartRideRepository;
        private readonly GenericRepository<COBRAMedicalPlan> cobraMedicalPlanRepository;
        private readonly GenericRepository<EmployeeBenefitsFileUpload> employeeBenefitsFileUploadRepository;
        private readonly GenericRepository<CobraBenefit> cobraBenefitRepository;
        private readonly GenericRepository<EmployeeBenefit> employeeBenefitRepository;
        private readonly ClarityDbContext _context;

        private readonly GenericRepository<CompanyStatus> statusRespository;
        private readonly GenericRepository<COBRADentalPlan> cobraDentalPlanRepository;
        private readonly GenericRepository<COBRAVisionPlan> cobraVisionPlanRepository;
        private readonly GenericRepository<COBRAHRAPlan> cobraHRAPlanRepository;
        private readonly GenericRepository<COBRAFSAPlan> cobraFSAPlanRepository;
        private readonly GenericRepository<COBRAInsurancePlan> cobraInsurancePlanRepository;
        private readonly GenericRepository<COBRAEAPPlan> cobraEAPPlanRepository;
        private readonly GenericRepository<MedicalPlanCoverageRate> medicalPlanCoverageRateRepository;
        private readonly GenericRepository<DentalCoverageRate> dentalCoverageRateRepository;
        private readonly GenericRepository<VisionCoverageRate> visionCoverageRateRepository;
        private readonly GenericRepository<HRACoverageRate> hraCoverageRateRepository;
        private readonly GenericRepository<FSACoverageRate> fsaCoverageRateRepository;
        private readonly GenericRepository<EAPCoverageRate> eapCoverageRateRepository;
        private readonly GenericRepository<InsurancePlanCoverageRate> insurancePlanCoverageRateRepository;
        private readonly GenericRepository<PayScheduleType> payScheduleTypeRepository;
        private readonly GenericRepository<CobraOpenEnrollmentManagement> cobraOpenEnrollmentManagementRepository;



        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public CompaniesController(ClarityDbContext context, IConfiguration configuration)
        {
            _context = context;
            cobraOpenEnrollmentManagementRepository = new GenericRepository<CobraOpenEnrollmentManagement>(context);

            repository = new GenericRepository<Company>(context);
            cobraGeneralInformationRepository = new GenericRepository<CobraGeneralInformation>(context);
            generalRepository = new GenericRepository<EmployeeBenefitsGeneralInformation>(context);
            companyDetailsRepository = new GenericRepository<CompanyDetails>(context);
            cobraClientContactRepository = new GenericRepository<COBRAClientContact>(context);
            cobraBrokerContactRepository = new GenericRepository<COBRABrokerContact>(context);
            addressRepository = new GenericRepository<Address>(context);
            companyContactRepository = new GenericRepository<CompanyContact>(context);
            brokerContactRepository = new GenericRepository<BrokerContact>(context);
            employeeBenefitsPlanRepository = new GenericRepository<EmployeeBenefitsPlan>(context);
            cobraPlanRepository = new GenericRepository<COBRAPlan>(context);
            employeeBenefitsFundingRepository = new GenericRepository<EmployeeBenefitsFunding>(context);
            cobraFundingRepository = new GenericRepository<COBRAFunding>(context);
            employeeBenefitsDCARepository = new GenericRepository<EmployeeBenefitsDCA>(context);
            employeeBenefitsLPFSARepository = new GenericRepository<EmployeeBenefitsLPFSA>(context);
            planTypeRepository = new GenericRepository<PlanType>(context);
            employeeBenefitsEnrollmentRepository = new GenericRepository<EmployeeBenefitsEnrollment>(context);
            employeeBenefitsFSARepository = new GenericRepository<EmployeeBenefitsFSA>(context);
            employeeBenefitsHRARepository = new GenericRepository<EmployeeBenefitsHRA>(context);
            employeeBenefitsHSARepository = new GenericRepository<EmployeeBenefitsHSA>(context);
            employeeBenefitsSmartRideRepository = new GenericRepository<EmployeeBenefitsSmartRide>(context);
            enrollmentAndEligibilityContactRepository = new GenericRepository<EnrollmentAndEligibilityContact>(context);
            cobraMedicalPlanRepository = new GenericRepository<COBRAMedicalPlan>(context);
            employeeBenefitsFileUploadRepository = new GenericRepository<EmployeeBenefitsFileUpload>(context);
            cobraBenefitRepository = new GenericRepository<CobraBenefit>(context);
            employeeBenefitRepository = new GenericRepository<EmployeeBenefit>(context);
            statusRespository = new GenericRepository<CompanyStatus>(context);
            cobraDentalPlanRepository = new GenericRepository<COBRADentalPlan>(context);
            cobraVisionPlanRepository = new GenericRepository<COBRAVisionPlan>(context);
            cobraHRAPlanRepository = new GenericRepository<COBRAHRAPlan>(context);
            cobraFSAPlanRepository = new GenericRepository<COBRAFSAPlan>(context);
            cobraInsurancePlanRepository = new GenericRepository<COBRAInsurancePlan>(context);
            cobraEAPPlanRepository = new GenericRepository<COBRAEAPPlan>(context);
            medicalPlanCoverageRateRepository = new GenericRepository<MedicalPlanCoverageRate>(context);
            dentalCoverageRateRepository = new GenericRepository<DentalCoverageRate>(context);
            visionCoverageRateRepository = new GenericRepository<VisionCoverageRate>(context);
            hraCoverageRateRepository = new GenericRepository<HRACoverageRate>(context);
            fsaCoverageRateRepository = new GenericRepository<FSACoverageRate>(context);
            eapCoverageRateRepository = new GenericRepository<EAPCoverageRate>(context);
            insurancePlanCoverageRateRepository = new GenericRepository<InsurancePlanCoverageRate>(context);
            payScheduleTypeRepository = new GenericRepository<PayScheduleType>(context);

            _httpClient = new HttpClient();
            _configuration = configuration;

        }

        // GET: api/Companies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Company>>> GetCompanies()
        {
            var baseURL = _configuration.GetValue<string>("BaseURL");

            var companies = await repository.GetAll();
            if (companies == null)
            {
                return NotFound();
            }
            //foreach (var company in companies)
            //{

            //    var response4 = await _httpClient.GetAsync(baseURL + "/CompanyContacts/ByCompanyId/" + company.Id);
            //    if (response4.IsSuccessStatusCode)
            //    {
            //        string apiResponse4 = await response4.Content.ReadAsStringAsync();
            //        var companyContacts = JsonConvert.DeserializeObject<List<CompanyContact>>(apiResponse4);
            //        company.CompanyContacts = companyContacts;
            //    }
            //}
            return Ok(companies);
        }

        [HttpGet("GetCompaniesByEmail/{email}")]
        public async Task<ActionResult<IEnumerable<Company>>> GetCompaniesByEmail(string email)
        {
            var companies = await (from acc in _context.Api_Case_Contacts
                                   join ac in _context.Companies on acc.CaseId equals ac.case_id
                                   where acc.ContactEmail == email
                                   && (acc.ContactIsImplementationContact == 1 || acc.IsCaseCollaborator == "Yes")
                                   select ac).ToListAsync();

            //var companies2 = await repository.GetAllByCompanyId(entity => entity.case_implementation_contact_email == email);
            
            //var combinedCompanies = companies.Union(companies2).ToList();

            if (companies == null)
            {
                return NotFound();
            }
            return Ok(companies);
        }

        // GET: api/Companies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> GetCompany(int id)
        {
            var baseURL = _configuration.GetValue<string>("BaseURL");

            var company = await repository.GetById(id);
            if (company == null)
            {
                return NotFound();
            }

            var response = await _httpClient.GetAsync(baseURL + "/Addresses/ByCompanyId/" + id);
            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                var address = JsonConvert.DeserializeObject<Address>(apiResponse);
                company.Address = address;
            }

            var responseX = await _httpClient.GetAsync(baseURL + "/CompanyDetails/ByCompanyId/" + id);
            if (responseX.IsSuccessStatusCode)
            {
                string apiResponse = await responseX.Content.ReadAsStringAsync();
                var CompanyDetails = JsonConvert.DeserializeObject<CompanyDetails>(apiResponse);
                company.CompanyDetails = CompanyDetails;
            }

            var response2 = await _httpClient.GetAsync(baseURL + "/CompanyDivisions/ByCompanyId/" + id);
            if (response2.IsSuccessStatusCode)
            {
                string apiResponse2 = await response2.Content.ReadAsStringAsync();
                var companyDivisions = JsonConvert.DeserializeObject<List<CompanyDivision>>(apiResponse2);
                company.CompanyDivisions = companyDivisions;
            }

            var response3 = await _httpClient.GetAsync(baseURL + "/AffiliatedCompanies/ByCompanyId/" + id);
            if (response3.IsSuccessStatusCode)
            {
                string apiResponse3 = await response3.Content.ReadAsStringAsync();
                var affiliatedCompanies = JsonConvert.DeserializeObject<List<AffiliatedCompany>>(apiResponse3);
                company.AffiliatedCompanies = affiliatedCompanies;
            }

            var contactsResponse = await _httpClient.GetAsync(baseURL + "/ApiCaseContacts/ByCompanyId/" + id);
            if (contactsResponse.IsSuccessStatusCode)
            {
                string apiContactsResponse = await contactsResponse.Content.ReadAsStringAsync();
                var allContacts = JsonConvert.DeserializeObject<List<Api_Case_Contact>>(apiContactsResponse);
                company.Api_Case_Contacts = allContacts;
            }

            //var response5 = await _httpClient.GetAsync(baseURL + "/BrokerContacts/ByCompanyId/" + id);
            //if (response5.IsSuccessStatusCode)
            //{
            //    string apiResponse5 = await response5.Content.ReadAsStringAsync();
            //    var brokerContacts = JsonConvert.DeserializeObject<List<BrokerContact>>(apiResponse5);
            //    company.BrokerContacts = brokerContacts;
            //}

            //var response4 = await _httpClient.GetAsync(baseURL + "/CompanyContacts/ByCompanyId/" + id);
            //if (response4.IsSuccessStatusCode)
            //{
            //    string apiResponse4 = await response4.Content.ReadAsStringAsync();
            //    var companyContacts = JsonConvert.DeserializeObject<List<CompanyContact>>(apiResponse4);
            //    company.CompanyContacts = companyContacts;
            //}

            var employeeBenefitsPlanJSON = await _httpClient.GetAsync(baseURL + "/EmployeeBenefitsPlans/ByCompanyId/" + id);
            if (employeeBenefitsPlanJSON.IsSuccessStatusCode)
            {
                string employeeBenefitsPlanResponse = await employeeBenefitsPlanJSON.Content.ReadAsStringAsync();
                var employeeBenefitsPlan = JsonConvert.DeserializeObject<EmployeeBenefitsPlan>(employeeBenefitsPlanResponse);
                company.EmployeeBenefitsPlan = employeeBenefitsPlan;
            }

            var cobraPlanJSON = await _httpClient.GetAsync(baseURL + "/COBRAPlans/ByCompanyId/" + id);
            if (cobraPlanJSON.IsSuccessStatusCode)
            {
                string cobraPlanResponse = await cobraPlanJSON.Content.ReadAsStringAsync();
                var cobraPlan = JsonConvert.DeserializeObject<COBRAPlan>(cobraPlanResponse);
                if (cobraPlan != null)
                {
                    company.COBRAPlan = cobraPlan;
                }

            }

            var COBRAFundingJSON = await _httpClient.GetAsync(baseURL + "/COBRAFundings/ByCompanyId/" + id);
            if (COBRAFundingJSON.IsSuccessStatusCode)
            {
                string COBRAFundingJSONResponse = await COBRAFundingJSON.Content.ReadAsStringAsync();
                var COBRAFunding = JsonConvert.DeserializeObject<COBRAFunding>(COBRAFundingJSONResponse);
                if (COBRAFunding != null)
                {
                    company.COBRAFunding = COBRAFunding;
                }

            }

            var employeeBenefitsFundingJSON = await _httpClient.GetAsync(baseURL + "/EmployeeBenefitsFundings/ByCompanyId/" + id);
            if (employeeBenefitsFundingJSON.IsSuccessStatusCode)
            {
                string employeeBenefitsFundingResponse = await employeeBenefitsFundingJSON.Content.ReadAsStringAsync();
                var employeeBenefitsFunding = JsonConvert.DeserializeObject<EmployeeBenefitsFunding>(employeeBenefitsFundingResponse);
                if (employeeBenefitsFunding != null)
                {
                    company.EmployeeBenefitsFunding = employeeBenefitsFunding;

                }
            }

            var employeeBenefitsFileUploadResponseJSON = await _httpClient.GetAsync(baseURL + "/EmployeeBenefitsFileUploads/ByCompanyId/" + id);
            if (employeeBenefitsFileUploadResponseJSON.IsSuccessStatusCode)
            {
                string employeeBenefitsFileUploadAPIResponse = await employeeBenefitsFileUploadResponseJSON.Content.ReadAsStringAsync();
                var employeeBenefitsFileUpload = JsonConvert.DeserializeObject<EmployeeBenefitsFileUpload>(employeeBenefitsFileUploadAPIResponse);
                if (employeeBenefitsFileUpload != null)
                {
                    company.EmployeeBenefitsFileUpload = employeeBenefitsFileUpload;

                }
            }

            return Ok(company);
        }

        // PUT: api/Companies/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany(int id, Company company)
        {
            if (id != company.Id)
            {
                return BadRequest();
            }

            var updatedCompany = await repository.Update(company, id);
            if (updatedCompany == null)
            {
                return NotFound();
            }
            return Ok(updatedCompany);
        }

        [HttpPost]
        public async Task<ActionResult<Company>> PostCompany(Company company)
        {
            company.CreatedOn = DateTime.Now;
            company.CompanyStatus = "Client Notified";

            var addedCompany = await repository.Add(company);
            if (addedCompany == null)
            {
                return NotFound();
            }
            company.CaseOwnerEmail = company.CaseOwnerEmail;
            company.CalendlyUserName = company.CaseOwnerName;
            company.TotalProgress = 0;
            company.PendingProgress = 100;
            company.ReviewStatus = "No Review";
            company.BenefitReviewStatus = "No Review";
            company.CobraReviewStatus = "No Review";
            company.FundingReviewStatus = "No Review";

            //CompanyStatus companyStatus = new CompanyStatus();
            //companyStatus.StatusType = "Client Notified";
            //companyStatus.CompanyId = company.Id;
            //await statusRespository.Add(companyStatus);

            CompanyDetails companyDetails = new CompanyDetails();
            companyDetails.CompanyId = addedCompany.Id;
            await companyDetailsRepository.Add(companyDetails);

            Address address = new Address();
            address.CreatedOn = DateTime.Now;
            address.CompanyId = addedCompany.Id;
            await addressRepository.Add(address);

            

            //CompanyContact companyContact = new CompanyContact();
            //companyContact.IsInvite = "false";
            //companyContact.CompanyId = addedCompany.Id;
            //await companyContactRepository.Add(companyContact);

            //BrokerContact brokerContact = new BrokerContact();
            //brokerContact.IsInvite = "false";
            //brokerContact.CompanyId = addedCompany.Id;
            //await brokerContactRepository.Add(brokerContact);

            EmployeeBenefitsPlan employeeBenefitsPlan = new EmployeeBenefitsPlan();
            employeeBenefitsPlan.IsMidYearPlan = "false";
            employeeBenefitsPlan.IsPriorYearPlan = "false";
            employeeBenefitsPlan.IsSamePlanEligibility = "false";
            employeeBenefitsPlan.CompanyId = addedCompany.Id;
            await employeeBenefitsPlanRepository.Add(employeeBenefitsPlan);

            EmployeeBenefitsGeneralInformation employeeBenefitsGeneralInformation = new EmployeeBenefitsGeneralInformation();
            employeeBenefitsGeneralInformation.EmployeeBenefitsPlanId = employeeBenefitsPlan.Id;
            await generalRepository.Add(employeeBenefitsGeneralInformation);

            EmployeeBenefitsEnrollment employeeBenefitsEnrollment = new EmployeeBenefitsEnrollment();
            employeeBenefitsEnrollment.EmployeeBenefitsPlanId = employeeBenefitsPlan.Id;
            await employeeBenefitsEnrollmentRepository.Add(employeeBenefitsEnrollment);

            EmployeeBenefitsFSA employeeBenefitsFSA = new EmployeeBenefitsFSA();
            employeeBenefitsFSA.IsHSA = "true";
            employeeBenefitsFSA.IsOfferLPFSA = "true";
            employeeBenefitsFSA.IsFSAChecked = "true";
            employeeBenefitsFSA.IsStandardPlan = "true";
            employeeBenefitsFSA.AllowCarryOver = "true";
            employeeBenefitsFSA.AllowGracePeriod = "false";
            employeeBenefitsFSA.MinAnnualElectionAmount = "0";
            employeeBenefitsFSA.MaxAnnualElectionAmount = "3200";
            employeeBenefitsFSA.EmployerContribution = "0";
            employeeBenefitsFSA.RunOutPeriod = "Planning Period";
            employeeBenefitsFSA.ActiveEmployeeRunoutPeriod = "90 Days";
            employeeBenefitsFSA.TerminatedEmployeeRunoutPeriod = "90 Days";
            employeeBenefitsFSA.TerminatedEmployeeCoverageEndPeriod = "Date of Termination";
            employeeBenefitsFSA.EmployeeBenefitsPlanId = employeeBenefitsPlan.Id;
            await employeeBenefitsFSARepository.Add(employeeBenefitsFSA);

            EmployeeBenefitsDCA employeeBenefitsDCA = new EmployeeBenefitsDCA();
            employeeBenefitsDCA.IsDCAChecked = "true";
            employeeBenefitsDCA.IsStandardPlan = "true";
            employeeBenefitsDCA.TotalDays = 75;
            employeeBenefitsDCA.IsDCAChecked = "true";
            employeeBenefitsDCA.AllowGracePeriod = "true";
            employeeBenefitsDCA.ActiveEmployeeRunoutPeriod = "90 Days";
            employeeBenefitsDCA.TerminatedEmployeeRunoutPeriod = "90 Days";
            employeeBenefitsDCA.MinAnnualElectionAmount = "0";
            employeeBenefitsDCA.MaxAnnualElectionAmount = "5000";
            employeeBenefitsDCA.RunOutPeriod = "Planning Period";
            employeeBenefitsDCA.EmployerContribution = "0";
            employeeBenefitsDCA.TerminatedEmployeeCoverageEndPeriod = "End of Plan Year";
            employeeBenefitsDCA.EmployeeBenefitsFSAId = employeeBenefitsFSA.Id;
            await employeeBenefitsDCARepository.Add(employeeBenefitsDCA);

            EmployeeBenefitsLPFSA employeeBenefitsLPFSA = new EmployeeBenefitsLPFSA();
            employeeBenefitsLPFSA.IsLPFSAChecked = "true";
            employeeBenefitsLPFSA.IsStandardPlan = "true";
            employeeBenefitsLPFSA.AllowCarryOver = "true";
            employeeBenefitsLPFSA.AllowGracePeriod = "false";
            employeeBenefitsLPFSA.ActiveEmployeeRunoutPeriod = "90 Days";
            employeeBenefitsLPFSA.TerminatedEmployeeRunoutPeriod = "90 Days";
            employeeBenefitsLPFSA.RunOutPeriod = "Planning Period";
            employeeBenefitsLPFSA.MinAnnualElectionAmount = "0";
            employeeBenefitsLPFSA.MaxAnnualElectionAmount = "3050";
            employeeBenefitsLPFSA.EmployerContribution = "0";
            employeeBenefitsLPFSA.TerminatedEmployeeCoverageEndPeriod = "Date of Termination";
            employeeBenefitsLPFSA.EmployeeBenefitsFSAId = employeeBenefitsFSA.Id;
            await employeeBenefitsLPFSARepository.Add(employeeBenefitsLPFSA);

            EmployeeBenefitsHRA employeeBenefitsHRA = new EmployeeBenefitsHRA();
            employeeBenefitsHRA.ReimbursableExpenses = "0";
            employeeBenefitsHRA.HRAUnusedFund = "Forfeited";
            employeeBenefitsHRA.IsProRated = "false";
            employeeBenefitsHRA.IsEmployeeResponsible = "false";
            employeeBenefitsHRA.IsHRAEmployeeResponsible = "false";
            employeeBenefitsHRA.IsParticipantResponsible = "false";
            employeeBenefitsHRA.IsHRAorFSA = "FSA";
            employeeBenefitsHRA.IsPlanForClarityBenefitCard = "false";
            employeeBenefitsHRA.IsDependentCard = "true";
            employeeBenefitsHRA.HRAType = "Standard Integrated";
            employeeBenefitsHRA.ActiveEmployeeRunoutPeriod = "90 Days";
            employeeBenefitsHRA.TerminatedEmployeeRunoutPeriod = "90 Days";
            employeeBenefitsHRA.IsDependentCardOption = "Spouse Only";
            employeeBenefitsHRA.EmployeeBenefitsPlanId = employeeBenefitsPlan.Id;
            await employeeBenefitsHRARepository.Add(employeeBenefitsHRA);

            EmployeeBenefitsHSA employeeBenefitsHSA = new EmployeeBenefitsHSA();
            employeeBenefitsHSA.EnrolledEmployee = "";
            employeeBenefitsHSA.IsOfferHSA = "false";
            employeeBenefitsHSA.IsBulkTransfer = "false";
            employeeBenefitsHSA.IsPayrollAdvance = "false";
            employeeBenefitsHSA.IsFullBalanceAdvance = "false";
            employeeBenefitsHSA.IsEmployerContribution = "false";
            employeeBenefitsHSA.IsSpouseOrDependent = "false";
            employeeBenefitsHSA.IsSpouseOrDependent = "Spouse Only";
            employeeBenefitsHSA.MaximumAmount = "1000";
            employeeBenefitsHSA.EmployeeBenefitsPlanId = employeeBenefitsPlan.Id;
            await employeeBenefitsHSARepository.Add(employeeBenefitsHSA);

            PlanType planType = new PlanType();
            planType.EmployeeBenefitsPlanId = employeeBenefitsPlan.Id;
            await planTypeRepository.Add(planType);

            PayScheduleType payScheduleType = new PayScheduleType();
            payScheduleType.EmployeeBenefitsPlanId = employeeBenefitsPlan.Id;
            await payScheduleTypeRepository.Add(payScheduleType);

            EmployeeBenefitsSmartRide employeeBenefitsSmartRide = new EmployeeBenefitsSmartRide();
            employeeBenefitsSmartRide.IsParkingChecked = "true";
            employeeBenefitsSmartRide.IsTransitChecked = "true";
            employeeBenefitsSmartRide.IsParkingAddPlan = "true";
            employeeBenefitsSmartRide.IsTransitAddPlan = "true";
            employeeBenefitsSmartRide.IsParkingPostTax = "false";
            employeeBenefitsSmartRide.IsTransitPostTax = "false";
            employeeBenefitsSmartRide.ParkingRunoutPeriod = "90 Days";
            employeeBenefitsSmartRide.TransitRunoutPeriod = "90 Days";
            employeeBenefitsSmartRide.EmployeeBenefitsPlanId = employeeBenefitsPlan.Id;
            await employeeBenefitsSmartRideRepository.Add(employeeBenefitsSmartRide);

            COBRAPlan cobraPlan = new COBRAPlan();
            cobraPlan.IsOpenEnrollment = "false";
            cobraPlan.CompanyId = addedCompany.Id;
            await cobraPlanRepository.Add(cobraPlan);

            CobraGeneralInformation cobraGeneralInformation = new CobraGeneralInformation();
            cobraGeneralInformation.COBRAPlanId = cobraPlan.Id;
            await cobraGeneralInformationRepository.Add(cobraGeneralInformation);

            COBRAMedicalPlan cobraMedicalPlan = new COBRAMedicalPlan();
            cobraMedicalPlan.IsDisabilityExtension = "false";
            cobraMedicalPlan.IsDivisionSpecific = "false";
            cobraMedicalPlan.COBRAPlanId = cobraPlan.Id;
            await cobraMedicalPlanRepository.Add(cobraMedicalPlan);

            COBRADentalPlan cobraDentalPlan = new COBRADentalPlan();
            cobraDentalPlan.IsDisabilityExtension = "false";
            cobraDentalPlan.IsDivisionSpecific = "false";
            cobraDentalPlan.COBRAPlanId = cobraPlan.Id;
            await cobraDentalPlanRepository.Add(cobraDentalPlan);

            COBRAVisionPlan cobraVisionPlan = new COBRAVisionPlan();
            cobraVisionPlan.IsDisabilityExtension = "false";
            cobraVisionPlan.IsDivisionSpecific = "false";
            cobraVisionPlan.COBRAPlanId = cobraPlan.Id;
            await cobraVisionPlanRepository.Add(cobraVisionPlan);

            COBRAHRAPlan cobraHRAPlan = new COBRAHRAPlan();
            cobraHRAPlan.IsDisabilityExtension = "false";
            cobraHRAPlan.IsDivisionSpecific = "false";
            cobraHRAPlan.COBRAPlanId = cobraPlan.Id;
            await cobraHRAPlanRepository.Add(cobraHRAPlan);

            COBRAFSAPlan cobraFSAPlan = new COBRAFSAPlan();
            cobraFSAPlan.IsDisabilityExtension = "false";
            cobraFSAPlan.IsDivisionSpecific = "false";
            cobraFSAPlan.COBRAPlanId = cobraPlan.Id;
            await cobraFSAPlanRepository.Add(cobraFSAPlan);

            COBRAEAPPlan cobraEAPPlan = new COBRAEAPPlan();
            cobraEAPPlan.IsDisabilityExtension = "false";
            cobraEAPPlan.IsDivisionSpecific = "false";
            cobraEAPPlan.COBRAPlanId = cobraPlan.Id;
            await cobraEAPPlanRepository.Add(cobraEAPPlan);

            COBRAInsurancePlan cobraInsurancePlan = new COBRAInsurancePlan();
            cobraInsurancePlan.IsDisabilityExtension = "false";
            cobraInsurancePlan.IsDivisionSpecific = "false";
            cobraInsurancePlan.COBRAPlanId = cobraPlan.Id;
            await cobraInsurancePlanRepository.Add(cobraInsurancePlan);

            MedicalPlanCoverageRate medicalPlanCoverageRate = new MedicalPlanCoverageRate();
            medicalPlanCoverageRate.COBRAMedicalPlanId = cobraMedicalPlan.Id;
            await medicalPlanCoverageRateRepository.Add(medicalPlanCoverageRate);

            DentalCoverageRate dentalCoverageRate = new DentalCoverageRate();
            dentalCoverageRate.DentalPlanId = cobraDentalPlan.Id;
            await dentalCoverageRateRepository.Add(dentalCoverageRate);

            VisionCoverageRate visionCoverageRate = new VisionCoverageRate();
            visionCoverageRate.VisionPlanId = cobraVisionPlan.Id;
            await visionCoverageRateRepository.Add(visionCoverageRate);

            HRACoverageRate hraCoverageRate = new HRACoverageRate();
            hraCoverageRate.HRAPlanId = cobraHRAPlan.Id;
            await hraCoverageRateRepository.Add(hraCoverageRate);

            FSACoverageRate fsaCoverageRate = new FSACoverageRate();
            fsaCoverageRate.FSAPlanId = cobraFSAPlan.Id;
            await fsaCoverageRateRepository.Add(fsaCoverageRate);

            EAPCoverageRate eapCoverageRate = new EAPCoverageRate();
            eapCoverageRate.EAPPlanId = cobraEAPPlan.Id;
            await eapCoverageRateRepository.Add(eapCoverageRate);

            InsurancePlanCoverageRate insurancePlanCoverageRate = new InsurancePlanCoverageRate();
            insurancePlanCoverageRate.InsurancePlanId = cobraInsurancePlan.Id;
            await insurancePlanCoverageRateRepository.Add(insurancePlanCoverageRate);

            EnrollmentAndEligibilityContact enrollmentAndEligibilityContact = new EnrollmentAndEligibilityContact();
            enrollmentAndEligibilityContact.ContactType = "clarity";
            enrollmentAndEligibilityContact.COBRAPlanId = cobraPlan.Id;
            await enrollmentAndEligibilityContactRepository.Add(enrollmentAndEligibilityContact);

            CobraOpenEnrollmentManagement cobraOpenEnrollmentManagement = new CobraOpenEnrollmentManagement();
            cobraOpenEnrollmentManagement.COBRAPlanId = cobraPlan.Id;
            await cobraOpenEnrollmentManagementRepository.Add(cobraOpenEnrollmentManagement);


            COBRAFunding cobraFunding = new COBRAFunding();
            cobraFunding.COBRAPremiumProvider = "Employer";
            cobraFunding.IsBrokerorPartnerPayment = "false";
            cobraFunding.IsCreditCobraPremiumRemittance = "true";
            cobraFunding.IsDebitMonthlyAdministrationFee = "true";
            cobraFunding.CompanyId = addedCompany.Id;
            await cobraFundingRepository.Add(cobraFunding);

            EmployeeBenefitsFunding employeeBenefitsFunding = new EmployeeBenefitsFunding();
            employeeBenefitsFunding.IsBrokerorPartnerPayment = "false";
            employeeBenefitsFunding.IsBrokerorPartnerPaymentForClarity = "false";
            employeeBenefitsFunding.IsBrokerorPartnerPaymentForClient = "false";
            employeeBenefitsFunding.IsDebitConsumerBenefitFunding = "true";
            employeeBenefitsFunding.IsDebitConsumerBenefitFundingForClient = "true";
            employeeBenefitsFunding.IsDebitConsumerBenefitFundingForClarity = "true";
            employeeBenefitsFunding.IsDebitMonthlyAdministrationFee = "true";
            employeeBenefitsFunding.IsDebitMonthlyAdministrationFeeForClient = "true";
            employeeBenefitsFunding.IsDebitMonthlyAdministrationFeeForClarity = "true";

            employeeBenefitsFunding.CompanyId = addedCompany.Id;
            await employeeBenefitsFundingRepository.Add(employeeBenefitsFunding);

            EmployeeBenefitsFileUpload employeeBenefitsFileUpload = new EmployeeBenefitsFileUpload();
            employeeBenefitsFileUpload.PlanName = "consumerBenefits";
            employeeBenefitsFileUpload.CompanyId = addedCompany.Id;
            await employeeBenefitsFileUploadRepository.Add(employeeBenefitsFileUpload);

            string[] fileCategories = {
                "Age/Gender Banded Rates",
                "Summary of Benefits and Coverage (SBC) Documents",
                "Medical Carrier Invoice",
                "COBRA Subsidy Template",
                "COBRA Takeover File"
            };

            for (int i = 0; i < fileCategories.Length; i++)
            {
                CobraBenefit cobraBenefit = new CobraBenefit();
                cobraBenefit.FileId = employeeBenefitsFileUpload.Id;
                cobraBenefit.FileCategory = fileCategories[i];
                await cobraBenefitRepository.Add(cobraBenefit);
            }

            string[] benefitsFileCategories = {
                "Employee Benefits File Upload"
            };

            for (int i = 0; i < benefitsFileCategories.Length; i++)
            {
                EmployeeBenefit employeeBenefit = new EmployeeBenefit();
                employeeBenefit.FileId = employeeBenefitsFileUpload.Id;
                employeeBenefit.FileCategory = benefitsFileCategories[i];
                await employeeBenefitRepository.Add(employeeBenefit);
            }

            return RedirectToAction("GetCompany", new { id = addedCompany.Id });
        }


        [HttpGet("IsCobra/{id}")]
        public async Task<IActionResult> GetCOBRA(int id)
        {
            COBRAPlan COBRA = await cobraPlanRepository.GetByCompanyId(entity => entity.CompanyId == id);
            if (COBRA == null)
            {
                return NotFound();
            }
            return Ok(new ClarityResponse { Id = COBRA.Id, Progress = COBRA.Progress, Status = "Success" });
        }

        [HttpGet("IsEmployeeBenefit/{id}")]
        public async Task<IActionResult> GetEmployeeBenefit(int id)
        {
            EmployeeBenefitsPlan employeeBenefitsPlan = await employeeBenefitsPlanRepository.GetByCompanyId(entity => entity.CompanyId == id);
            if (employeeBenefitsPlan == null)
            {
                return NotFound();
            }
            return Ok(new ClarityResponse { Id = employeeBenefitsPlan.Id, Progress = employeeBenefitsPlan.Progress, Status = "Success" });
        }

        [HttpGet("CompanyProfileProgress/{id}")]
        public async Task<IActionResult> GetCompanyProfileProgress(int id)
        {
            Company company = await repository.GetByCompanyId(entity => entity.Id == id);
            if (company == null)
            {
                return NotFound();
            }
            return Ok(new ClarityResponse { Id = company.Id, Progress = company.Progress, Status = company.Status });
        }

        [HttpGet("CompanyProfileStatus/{id}")]
        public async Task<IActionResult> GetCompanyProfileStatus(int id)
        {
            Company company = await repository.GetByCompanyId(entity => entity.Id == id);
            if (company == null)
            {
                return NotFound();
            }
            return Ok(new ClarityResponse { Id = company.Id, Progress = company.Progress, Status = company.Status });
        }

        [HttpGet("OverallStatus/{id}")]
        public async Task<IActionResult> GetCompanyProfileOverallStatus(int id)
        {
            Company company = await repository.GetByCompanyId(entity => entity.Id == id);
            if (company == null)
            {
                return NotFound();
            }
            return Ok(new ClarityResponse { Id = company.Id, Progress = company.Progress, Status = company.CompanyStatus });
        }

        [HttpGet("FundingProgress/{id}")]
        public async Task<IActionResult> GetFundingProgress(int id)
        {
            int employeeBenefitsFundingProgress = 100;
            int cobraFundingProgress = 100;
            EmployeeBenefitsFunding employeeBenefitsFunding = await employeeBenefitsFundingRepository.GetByCompanyId(entity => entity.Id == id);
            if (employeeBenefitsFunding != null)
            {
                employeeBenefitsFundingProgress = employeeBenefitsFunding.Progress;
            }
            COBRAFunding cobraFunding = await cobraFundingRepository.GetByCompanyId(entity => entity.Id == id);
            if (cobraFunding != null)
            {
                cobraFundingProgress = cobraFunding.Progress;
            }
            int progress = (int)(employeeBenefitsFundingProgress + cobraFundingProgress) / 2;
            return Ok(new ClarityResponse { Id = id, Progress = progress, Status = "Success" });
        }

        [HttpGet("TotalPlanProgress/{id}")]
        public async Task<IActionResult> GetTotalPlanProgress(int id)
        {
            Company company = await repository.GetByCompanyId(entity => entity.Id == id);
            if (company == null)
            {
                return NotFound();
            }
            return Ok(new ClarityResponse { Id = company.Id, Progress = company.TotalPlanProgress, Status = "Success" });
        }

    }
}
