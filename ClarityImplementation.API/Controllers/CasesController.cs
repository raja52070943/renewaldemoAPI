using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models;

using ClarityImplementation.API.Models.FileUpload;
using ClarityImplementation.API.Models.Funding;
using ClarityImplementation.API.Models.Plans;
using ClarityImplementation.API.Models.Plans.COBRA;
using ClarityImplementation.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Configuration;
using System.Net.Http;
using System.Text;

namespace ClarityImplementation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CasesController : ControllerBase
    {
        private readonly GenericRepository<Api_Case> repository;
        

        public CasesController(ClarityDbContext context, IConfiguration configuration) {
            repository = new GenericRepository<Api_Case>(context);
            
        }
        // GET: api/Cases
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Api_Case>>> GetCases()
        {
            var cases = await repository.GetAllByStatus(record => record.CaseStatus == "New");
            if (cases == null)
            {
                return NotFound();
            }
            //foreach (ApiCase apiCase in cases)
            //{
            //    Company company = new Company();
            //    var addedCompany = await companyRepository.Add(company);
            //    CompanyDetails companyDetails = new CompanyDetails()

            //    { Name = apiCase.legal_business_name, ImplementationPlanType = apiCase.implementation_plan_type, ImplementationDate = apiCase.implementation_date, CreatedOn = apiCase.created_at };
            //    company.CreatedOn = DateTime.Now;
            //    company.CompanyStatus = "Client Notified";
            //    company.case_implementation_contact_first_name = "Olivia";
            //    company.case_implementation_contact_last_name = "Morris";
            //    company.ReviewStatus = "No Review";
            //    company.BenefitReviewStatus = "No Review";
            //    company.CobraReviewStatus = "No Review";
            //    company.FundingReviewStatus = "No Review";

            //    companyDetails.CompanyId = addedCompany.Id;
            //    await companyDetailsRepository.Add(companyDetails);

            //    var emailModel = new Email
            //    {
            //        ToEmail = "noreply@claritybenefitsolutions.com",
            //        Subject = "Please Register for the Clarity Benefit Solutions Portal and Begin Your Implementation - New Implementation",
            //        CompanyId = addedCompany.Id,
            //        RecipientName = addedCompany.CompanyDetails.Name,
            //        FirstName = addedCompany.case_implementation_contact_first_name,
            //        LastName = addedCompany.case_implementation_contact_last_name,
            //        Template = "EmailCopy1.html",
            //        NoOfDays = 0,
            //        NoOfReminders = 2
            //    };

            //    var json = JsonConvert.SerializeObject(emailModel);
            //    var content = new StringContent(json, Encoding.UTF8, "application/json");

            //    var baseURL = _configuration.GetValue<string>("BaseURL");
            //    var sendEmail = await _httpClient.PostAsync(baseURL + "/Email/send", content);
            //    if (sendEmail.IsSuccessStatusCode)
            //    {
            //        return Ok(sendEmail);
            //    }
            //    company.CaseOwnerEmail = "dunderwood@claritybenefitsolutions.com";
            //    company.CalendlyUserName = "dunderwood";
            //    company.TotalProgress = 0;
            //    company.PendingProgress = 100;

            //    //CompanyStatus companyStatus = new CompanyStatus();
            //    //companyStatus.StatusType = "Client Notified";
            //    //companyStatus.CompanyId = company.Id;
            //    //await statusRespository.Add(companyStatus);

            //    Address address = new Address();
            //    address.CreatedOn = DateTime.Now;
            //    address.CompanyId = addedCompany.Id;
            //    address.AddressLine1 = apiCase.street_physical_address;
            //    address.AddressLine2 = apiCase.street_line2;
            //    address.State = apiCase.state; 
            //    address.City = apiCase.city;
            //    address.Zip = apiCase.zip;


            //    await addressRepository.Add(address);
            //    //CompanyContact companyContact = new CompanyContact();
            //    //companyContact.IsInvite = "false";
            //    //companyContact.CompanyId = addedCompany.Id;
            //    //await companyContactRepository.Add(companyContact);

            //    //BrokerContact brokerContact = new BrokerContact();
            //    //brokerContact.IsInvite = "false";
            //    //brokerContact.CompanyId = addedCompany.Id;
            //    //await brokerContactRepository.Add(brokerContact);

            //    EmployeeBenefitsFileUpload employeeBenefitsFileUpload = new EmployeeBenefitsFileUpload();
            //    employeeBenefitsFileUpload.PlanName = "consumerBenefits";
            //    employeeBenefitsFileUpload.CompanyId = addedCompany.Id;
            //    await employeeBenefitsFileUploadRepository.Add(employeeBenefitsFileUpload);

            //    string[] fileCategories = {
            //        "Summary of Benefits and Coverage (SBC) Documents",
            //        "Medical Carrier Invoice",
            //        "Age/Gender Banded Rates",
            //        "COBRA Subsidy Template",
            //        "COBRA QB Enrollment Template"
            //    };

            //    for (int i = 0; i < fileCategories.Length; i++)
            //    {
            //        CobraBenefit cobraBenefit = new CobraBenefit();
            //        cobraBenefit.FileId = employeeBenefitsFileUpload.Id;
            //        cobraBenefit.FileCategory = fileCategories[i];
            //        await cobraBenefitRepository.Add(cobraBenefit);
            //    }

            //    string[] benefitsFileCategories = {
            //        "Employee Benefits File Upload"
            //    };

            //    for (int i = 0; i < benefitsFileCategories.Length; i++)
            //    {
            //        EmployeeBenefit employeeBenefit = new EmployeeBenefit();
            //        employeeBenefit.FileId = employeeBenefitsFileUpload.Id;
            //        employeeBenefit.FileCategory = benefitsFileCategories[i];
            //        await employeeBenefitRepository.Add(employeeBenefit);
            //    }

            //    string implementationPlans = apiCase.implementation_plan_type;
            //    if(implementationPlans != null)
            //    {
            //        string[] plans = implementationPlans.Split(new char[] { ';', ',', '|' });


            //        if (plans.Contains("COBRA"))
            //        {
            //            COBRAPlan cobraPlan = new COBRAPlan();
            //            cobraPlan.IsOpenEnrollment = "false";
            //            cobraPlan.CompanyId = addedCompany.Id;
            //            await cobraPlanRepository.Add(cobraPlan);

            //            CobraGeneralInformation cobraGeneralInformation = new CobraGeneralInformation();
            //            cobraGeneralInformation.COBRAAdminister = "In House";
            //            cobraGeneralInformation.IsOfferSubsidizedPremiums = "false";
            //            cobraGeneralInformation.COBRAPlanId = cobraPlan.Id;
            //            await cobraGeneralInformationRepository.Add(cobraGeneralInformation);

            //            COBRAMedicalPlan cobraMedicalPlan = new COBRAMedicalPlan();
            //            cobraMedicalPlan.IsDisabilityExtension = "false";
            //            cobraMedicalPlan.COBRAPlanId = cobraPlan.Id;
            //            await cobraMedicalPlanRepository.Add(cobraMedicalPlan);

            //            COBRADentalPlan cobraDentalPlan = new COBRADentalPlan();
            //            cobraDentalPlan.IsDisabilityExtension = "false";
            //            cobraDentalPlan.IsDivisionSpecific = "false";
            //            cobraDentalPlan.COBRAPlanId = cobraPlan.Id;
            //            await cobraDentalPlanRepository.Add(cobraDentalPlan);

            //            COBRAVisionPlan cobraVisionPlan = new COBRAVisionPlan();
            //            cobraVisionPlan.IsDisabilityExtension = "false";
            //            cobraVisionPlan.IsDivisionSpecific = "false";
            //            cobraVisionPlan.COBRAPlanId = cobraPlan.Id;
            //            await cobraVisionPlanRepository.Add(cobraVisionPlan);

            //            COBRAHRAPlan cobraHRAPlan = new COBRAHRAPlan();
            //            cobraHRAPlan.IsDisabilityExtension = "false";
            //            cobraHRAPlan.IsDivisionSpecific = "false";
            //            cobraHRAPlan.COBRAPlanId = cobraPlan.Id;
            //            await cobraHRAPlanRepository.Add(cobraHRAPlan);

            //            COBRAFSAPlan cobraFSAPlan = new COBRAFSAPlan();
            //            cobraFSAPlan.IsDisabilityExtension = "false";
            //            cobraFSAPlan.IsDivisionSpecific = "false";
            //            cobraFSAPlan.COBRAPlanId = cobraPlan.Id;
            //            await cobraFSAPlanRepository.Add(cobraFSAPlan);

            //            COBRAInsurancePlan cobraInsurancePlan = new COBRAInsurancePlan();
            //            cobraInsurancePlan.IsDisabilityExtension = "false";
            //            cobraInsurancePlan.IsDivisionSpecific = "false";
            //            cobraInsurancePlan.COBRAPlanId = cobraPlan.Id;
            //            await cobraInsurancePlanRepository.Add(cobraInsurancePlan);

            //            COBRAEAPPlan cobraEAPPlan = new COBRAEAPPlan();
            //            cobraEAPPlan.IsDisabilityExtension = "false";
            //            cobraEAPPlan.IsDivisionSpecific = "false";
            //            cobraEAPPlan.COBRAPlanId = cobraPlan.Id;
            //            await cobraEAPPlanRepository.Add(cobraEAPPlan);

            //            MedicalPlanCoverageRate medicalPlanCoverageRate = new MedicalPlanCoverageRate();
            //            medicalPlanCoverageRate.COBRAMedicalPlanId = cobraMedicalPlan.Id;
            //            await medicalPlanCoverageRateRepository.Add(medicalPlanCoverageRate);

            //            DentalCoverageRate dentalCoverageRate = new DentalCoverageRate();
            //            dentalCoverageRate.DentalPlanId = cobraDentalPlan.Id;
            //            await dentalCoverageRateRepository.Add(dentalCoverageRate);

            //            VisionCoverageRate visionCoverageRate = new VisionCoverageRate();
            //            visionCoverageRate.VisionPlanId = cobraVisionPlan.Id;
            //            await visionCoverageRateRepository.Add(visionCoverageRate);

            //            HRACoverageRate hraCoverageRate = new HRACoverageRate();
            //            hraCoverageRate.HRAPlanId = cobraHRAPlan.Id;
            //            await hraCoverageRateRepository.Add(hraCoverageRate);

            //            FSACoverageRate fsaCoverageRate = new FSACoverageRate();
            //            fsaCoverageRate.FSAPlanId = cobraFSAPlan.Id;
            //            await fsaCoverageRateRepository.Add(fsaCoverageRate);

            //            EAPCoverageRate eapCoverageRate = new EAPCoverageRate();
            //            eapCoverageRate.EAPPlanId = cobraEAPPlan.Id;
            //            await eapCoverageRateRepository.Add(eapCoverageRate);

            //            InsurancePlanCoverageRate insurancePlanCoverageRate = new InsurancePlanCoverageRate();
            //            insurancePlanCoverageRate.InsurancePlanId = cobraInsurancePlan.Id;
            //            await insurancePlanCoverageRateRepository.Add(insurancePlanCoverageRate);

            //            EnrollmentAndEligibilityContact enrollmentAndEligibilityContact = new EnrollmentAndEligibilityContact();
            //            enrollmentAndEligibilityContact.ContactType = "clarity";
            //            enrollmentAndEligibilityContact.COBRAPlanId = cobraPlan.Id;
            //            await enrollmentAndEligibilityContactRepository.Add(enrollmentAndEligibilityContact);

            //            COBRAClientContact cobraClientContact = new COBRAClientContact();
            //            cobraClientContact.EnrollmentAndEligibilityContactId = enrollmentAndEligibilityContact.Id;
            //            await cobraClientContactRepository.Add(cobraClientContact);

            //            COBRABrokerContact cobraBrokerContact = new COBRABrokerContact();
            //            cobraBrokerContact.EnrollmentAndEligibilityContactId = enrollmentAndEligibilityContact.Id;
            //            await cobraBrokerContactRepository.Add(cobraBrokerContact);

            //            COBRAFunding cobraFunding = new COBRAFunding();
            //            cobraFunding.COBRAPremiumProvider = "Employer";
            //            cobraFunding.CompanyId = addedCompany.Id;
            //            await cobraFundingRepository.Add(cobraFunding);
            //        }
            //        if(plans.Contains("HRA") || plans.Contains("FSA") || plans.Contains("HSA") ||(plans.Contains("Parking") && plans.Contains("Transit")))
            //        {
            //            EmployeeBenefitsPlan employeeBenefitsPlan = new EmployeeBenefitsPlan();
            //            employeeBenefitsPlan.IsMidYearPlan = "false";
            //            employeeBenefitsPlan.IsPriorYearPlan = "false";
            //            employeeBenefitsPlan.IsSamePlanEligibility = "false";
            //            employeeBenefitsPlan.CompanyId = addedCompany.Id;
            //            await employeeBenefitsPlanRepository.Add(employeeBenefitsPlan);

            //            EmployeeBenefitsGeneralInformation employeeBenefitsGeneralInformation = new EmployeeBenefitsGeneralInformation();
            //            employeeBenefitsGeneralInformation.EmployeeBenefitsPlanId = employeeBenefitsPlan.Id;
            //            await generalRepository.Add(employeeBenefitsGeneralInformation);

            //            EmployeeBenefitsEnrollment employeeBenefitsEnrollment = new EmployeeBenefitsEnrollment();
            //            employeeBenefitsEnrollment.EmployeeBenefitsPlanId = employeeBenefitsPlan.Id;
            //            await employeeBenefitsEnrollmentRepository.Add(employeeBenefitsEnrollment);

            //            PlanType planType = new PlanType();
            //            planType.EmployeeBenefitsPlanId = employeeBenefitsPlan.Id;
            //            await planTypeRepository.Add(planType);

            //            PayScheduleType payScheduleType = new PayScheduleType();
            //            payScheduleType.EmployeeBenefitsPlanId = employeeBenefitsPlan.Id;
            //            await payScheduleTypeRepository.Add(payScheduleType);

            //            EmployeeBenefitsFunding employeeBenefitsFunding = new EmployeeBenefitsFunding();
            //            employeeBenefitsFunding.CompanyId = addedCompany.Id;
            //            await employeeBenefitsFundingRepository.Add(employeeBenefitsFunding);

            //            if (plans.Contains("HRA"))
            //            {
            //                EmployeeBenefitsHRA employeeBenefitsHRA = new EmployeeBenefitsHRA();
            //                employeeBenefitsHRA.ReimbursableExpenses = "0";
            //                employeeBenefitsHRA.HRAUnusedFund = "Forfeited";
            //                employeeBenefitsHRA.IsProRated = "false";
            //                employeeBenefitsHRA.IsEmployeeResponsible = "false";
            //                employeeBenefitsHRA.IsHRAEmployeeResponsible = "false";
            //                employeeBenefitsHRA.IsParticipantResponsible = "false";
            //                employeeBenefitsHRA.IsHRAorFSA = "FSA";
            //                employeeBenefitsHRA.IsPlanForClarityBenefitCard = "false";
            //                employeeBenefitsHRA.IsDependentCard = "true";
            //                employeeBenefitsHRA.HRAType = "Standard Integrated";
            //                employeeBenefitsHRA.ActiveEmployeeRunoutPeriod = "90 Days";
            //                employeeBenefitsHRA.TerminatedEmployeeRunoutPeriod = "90 Days";
            //                employeeBenefitsHRA.IsDependentCardOption = "Spouse Only";
            //                employeeBenefitsHRA.EmployeeBenefitsPlanId = employeeBenefitsPlan.Id;
            //                await employeeBenefitsHRARepository.Add(employeeBenefitsHRA);
            //            }
            //            if (plans.Contains("FSA"))
            //            {
            //                EmployeeBenefitsFSA employeeBenefitsFSA = new EmployeeBenefitsFSA();
            //                employeeBenefitsFSA.IsHSA = "true";
            //                employeeBenefitsFSA.IsOfferLPFSA = "true";
            //                employeeBenefitsFSA.IsFSAChecked = "true";
            //                employeeBenefitsFSA.IsStandardPlan = "true";
            //                employeeBenefitsFSA.AllowCarryOver = "true";
            //                employeeBenefitsFSA.RunOutPeriod = "Planning Period";
            //                employeeBenefitsFSA.AllowGracePeriod = "false";
            //                employeeBenefitsFSA.MinAnnualElectionAmount = "0";
            //                employeeBenefitsFSA.MaxAnnualElectionAmount = "3050";
            //                employeeBenefitsFSA.EmployerContribution = "0";
            //                employeeBenefitsFSA.ActiveEmployeeRunoutPeriod = "90 Days";
            //                employeeBenefitsFSA.TerminatedEmployeeRunoutPeriod = "90 Days";
            //                employeeBenefitsFSA.TerminatedEmployeeCoverageEndPeriod = "Date of Termination";
            //                employeeBenefitsFSA.EmployeeBenefitsPlanId = employeeBenefitsPlan.Id;
            //                await employeeBenefitsFSARepository.Add(employeeBenefitsFSA);

            //                if (plans.Contains("LPFSA"))
            //                {
            //                    EmployeeBenefitsLPFSA employeeBenefitsLPFSA = new EmployeeBenefitsLPFSA();
            //                    employeeBenefitsLPFSA.IsLPFSAChecked = "true";
            //                    employeeBenefitsLPFSA.IsStandardPlan = "true";
            //                    employeeBenefitsLPFSA.AllowCarryOver = "true";
            //                    employeeBenefitsLPFSA.AllowGracePeriod = "false";
            //                    employeeBenefitsLPFSA.ActiveEmployeeRunoutPeriod = "90 Days";
            //                    employeeBenefitsLPFSA.TerminatedEmployeeRunoutPeriod = "90 Days";
            //                    employeeBenefitsLPFSA.RunOutPeriod = "Planning Period";
            //                    employeeBenefitsLPFSA.MinAnnualElectionAmount = "0";
            //                    employeeBenefitsLPFSA.MaxAnnualElectionAmount = "3050";
            //                    employeeBenefitsLPFSA.EmployerContribution = "0";
            //                    employeeBenefitsLPFSA.TerminatedEmployeeCoverageEndPeriod = "Date of Termination";
            //                    employeeBenefitsLPFSA.EmployeeBenefitsFSAId = employeeBenefitsFSA.Id;
            //                    await employeeBenefitsLPFSARepository.Add(employeeBenefitsLPFSA);
            //                }
            //                if (plans.Contains("DCA"))
            //                {
            //                    EmployeeBenefitsDCA employeeBenefitsDCA = new EmployeeBenefitsDCA();
            //                    employeeBenefitsDCA.IsDCAChecked = "true";
            //                    employeeBenefitsDCA.IsStandardPlan = "true";
            //                    employeeBenefitsDCA.TotalDays = 75;
            //                    employeeBenefitsDCA.IsDCAChecked = "true";
            //                    employeeBenefitsDCA.AllowGracePeriod = "true";
            //                    employeeBenefitsDCA.ActiveEmployeeRunoutPeriod = "90 Days";
            //                    employeeBenefitsDCA.TerminatedEmployeeRunoutPeriod = "90 Days";
            //                    employeeBenefitsDCA.RunOutPeriod = "Planning Period";
            //                    employeeBenefitsDCA.MinAnnualElectionAmount = "0";
            //                    employeeBenefitsDCA.MaxAnnualElectionAmount = "5000";
            //                    employeeBenefitsDCA.EmployerContribution = "0";
            //                    employeeBenefitsDCA.TerminatedEmployeeCoverageEndPeriod = "End of Plan Year";
            //                    employeeBenefitsDCA.EmployeeBenefitsFSAId = employeeBenefitsFSA.Id;
            //                    await employeeBenefitsDCARepository.Add(employeeBenefitsDCA);
            //                }
            //            }

            //            if (plans.Contains("HSA"))
            //            {
            //                EmployeeBenefitsHSA employeeBenefitsHSA = new EmployeeBenefitsHSA();
            //                employeeBenefitsHSA.EnrolledEmployee = "";
            //                employeeBenefitsHSA.IsOfferHSA = "false";
            //                employeeBenefitsHSA.IsBulkTransfer = "false";
            //                employeeBenefitsHSA.IsPayrollAdvance = "false";
            //                employeeBenefitsHSA.IsFullBalanceAdvance = "false";
            //                employeeBenefitsHSA.IsEmployerContribution = "false";
            //                employeeBenefitsHSA.IsSpouseOrDependent = "false";
            //                employeeBenefitsHSA.IsSpouseOrDependent = "Spouse Only";
            //                employeeBenefitsHSA.MaximumAmount = "1000";
            //                employeeBenefitsHSA.EmployeeBenefitsPlanId = employeeBenefitsPlan.Id;
            //                await employeeBenefitsHSARepository.Add(employeeBenefitsHSA);
            //            }
            //            if (plans.Contains("Parking") && plans.Contains("Transit"))
            //            {
            //                EmployeeBenefitsSmartRide employeeBenefitsSmartRide = new EmployeeBenefitsSmartRide();
            //                employeeBenefitsSmartRide.IsParkingChecked = "true";
            //                employeeBenefitsSmartRide.IsTransitChecked = "true";
            //                employeeBenefitsSmartRide.IsParkingAddPlan = "true";
            //                employeeBenefitsSmartRide.IsTransitAddPlan = "true";
            //                employeeBenefitsSmartRide.IsParkingPostTax = "false";
            //                employeeBenefitsSmartRide.IsTransitPostTax = "false";
            //                employeeBenefitsSmartRide.ParkingRunoutPeriod = "90 Days";
            //                employeeBenefitsSmartRide.TransitRunoutPeriod = "90 Days";
            //                employeeBenefitsSmartRide.EmployeeBenefitsPlanId = employeeBenefitsPlan.Id;
            //                await employeeBenefitsSmartRideRepository.Add(employeeBenefitsSmartRide);
            //            }

            //        }
            //    }             
            //}
            return Ok(cases);
        }
    }
}
