using ClarityImplementation.API.Db;
using ClarityImplementation.API.Models;
using ClarityImplementation.API.Models.FileUpload;
using ClarityImplementation.API.Models.Funding;
using ClarityImplementation.API.Models.Plans.COBRA;
using ClarityImplementation.API.Models.Plans;
using ClarityImplementation.API.Repositories;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ClarityImplementation.API.Services.SFCases
{
    public class CasesService
    {
        // private readonly GenericRepository<Api_Case> repository;
        private readonly GenericRepository<CompanyDetails> companyDetailsRepository;
        private readonly GenericRepository<EmployeeBenefitsFileUploadItem> ebFileUploadItemRepository;
        private readonly GenericRepository<CobraGeneralInformation> cobraGeneralInformationRepository;
        private readonly GenericRepository<EmployeeBenefitsGeneralInformation> generalRepository;
        private readonly GenericRepository<Company> companyRepository;
        private readonly GenericRepository<Address> addressRepository;
        private readonly GenericRepository<CompanyContact> companyContactRepository;
        private readonly GenericRepository<BrokerContact> brokerContactRepository;
        private readonly GenericRepository<EmployeeBenefit> employeeBenefitRepository;
        private readonly GenericRepository<PlanType> planTypeRepository;
        private readonly GenericRepository<EmployeeBenefitsEnrollment> employeeBenefitsEnrollmentRepository;
        private readonly GenericRepository<COBRAClientContact> cobraClientContactRepository;
        private readonly GenericRepository<COBRABrokerContact> cobraBrokerContactRepository;
        private readonly GenericRepository<EmployeeBenefitsPlan> employeeBenefitsPlanRepository;
        private readonly GenericRepository<EmployeeBenefitsFunding> employeeBenefitsFundingRepository;
        private readonly GenericRepository<COBRAPlan> cobraPlanRepository;
        private readonly GenericRepository<EnrollmentAndEligibilityContact> enrollmentAndEligibilityContactRepository;
        private readonly GenericRepository<COBRAFunding> cobraFundingRepository;
        private readonly GenericRepository<EmployeeBenefitsDCA> employeeBenefitsDCARepository;
        private readonly GenericRepository<EmployeeBenefitsLPFSA> employeeBenefitsLPFSARepository;
        private readonly GenericRepository<EmployeeBenefitsFSA> employeeBenefitsFSARepository;
        private readonly GenericRepository<EmployeeBenefitsHRA> employeeBenefitsHRARepository;
        private readonly GenericRepository<EmployeeBenefitsHSA> employeeBenefitsHSARepository;
        private readonly GenericRepository<EmployeeBenefitsSmartRide> employeeBenefitsSmartRideRepository;
        private readonly GenericRepository<EmployeeBenefitsFileUpload> employeeBenefitsFileUploadRepository;
        private readonly GenericRepository<CobraBenefit> cobraBenefitRepository;
        private readonly GenericRepository<CompanyStatus> statusRespository;
        private readonly GenericRepository<COBRAMedicalPlan> cobraMedicalPlanRepository;
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

        public CasesService(Api_Case api_Case, ClarityDbContext context)
        {
            // repository = new GenericRepository<Api_Case>(context);
            cobraOpenEnrollmentManagementRepository = new GenericRepository<CobraOpenEnrollmentManagement>(context);

            generalRepository = new GenericRepository<EmployeeBenefitsGeneralInformation>(context);
            companyDetailsRepository = new GenericRepository<CompanyDetails>(context);
            cobraGeneralInformationRepository = new GenericRepository<CobraGeneralInformation>(context);
            addressRepository = new GenericRepository<Address>(context);
            companyContactRepository = new GenericRepository<CompanyContact>(context);
            ebFileUploadItemRepository = new GenericRepository<EmployeeBenefitsFileUploadItem>(context);
            brokerContactRepository = new GenericRepository<BrokerContact>(context);
            companyRepository = new GenericRepository<Company>(context);
            planTypeRepository = new GenericRepository<PlanType>(context);
            employeeBenefitRepository = new GenericRepository<EmployeeBenefit>(context);
            employeeBenefitsEnrollmentRepository = new GenericRepository<EmployeeBenefitsEnrollment>(context);
            employeeBenefitsPlanRepository = new GenericRepository<EmployeeBenefitsPlan>(context);
            cobraPlanRepository = new GenericRepository<COBRAPlan>(context);
            cobraClientContactRepository = new GenericRepository<COBRAClientContact>(context);
            cobraBrokerContactRepository = new GenericRepository<COBRABrokerContact>(context);
            employeeBenefitsDCARepository = new GenericRepository<EmployeeBenefitsDCA>(context);
            employeeBenefitsLPFSARepository = new GenericRepository<EmployeeBenefitsLPFSA>(context);
            employeeBenefitsFSARepository = new GenericRepository<EmployeeBenefitsFSA>(context);
            employeeBenefitsHRARepository = new GenericRepository<EmployeeBenefitsHRA>(context);
            employeeBenefitsHSARepository = new GenericRepository<EmployeeBenefitsHSA>(context);
            employeeBenefitsSmartRideRepository = new GenericRepository<EmployeeBenefitsSmartRide>(context);
            employeeBenefitsFundingRepository = new GenericRepository<EmployeeBenefitsFunding>(context);
            cobraFundingRepository = new GenericRepository<COBRAFunding>(context);
            enrollmentAndEligibilityContactRepository = new GenericRepository<EnrollmentAndEligibilityContact>(context);
            employeeBenefitsFileUploadRepository = new GenericRepository<EmployeeBenefitsFileUpload>(context);
            cobraBenefitRepository = new GenericRepository<CobraBenefit>(context);
            statusRespository = new GenericRepository<CompanyStatus>(context);
            cobraMedicalPlanRepository = new GenericRepository<COBRAMedicalPlan>(context);
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
        }

        public async Task<string> AddCase(Api_Case apiCase)
        {
            if (apiCase == null)
            {
                return null;
            }
            var existingCompany = await GetExistingCase(apiCase.CaseId);
            if (existingCompany != null)
            {
                existingCompany.case_implementation_contact_email = apiCase.Case_Implementation_Contact_Email;
                existingCompany.case_implementation_contact_first_name = apiCase.Case_Implementation_Contact_First_Name;
                existingCompany.case_implementation_contact_last_name = apiCase.Case_Implementation_Contact_Last_Name;
                existingCompany.CompanyStatus = "Client Notified";
                existingCompany.ImplementationPlans = apiCase.ImplementationPlanType;
                var updatedCompany = await companyRepository.Update(existingCompany, existingCompany.Id);
            }
            else
            {
                var newCase = await CreateNewCase(apiCase);
            }

            return "Success";
        }

        private async Task<Company> GetExistingCase(string caseId)
        {
            var existingCompany = await companyRepository.GetByCompanyId(company => company.case_id == caseId);
            if (existingCompany != null)
            {
                return existingCompany;
            }
            return null;
        }
        private async Task<string> CreateNewCase(Api_Case apiCase)
        {

            Company company = new Company();
            company.CaseOwnerName = apiCase.Case_Owner_Name;
            company.KickoffStatus = apiCase.Step9Status;
            company.CaseOwnerEmail = apiCase.Case_Owner_Email;
            company.case_implementation_contact_email = apiCase.Case_Implementation_Contact_Email;
            company.case_implementation_contact_first_name = apiCase.Case_Implementation_Contact_First_Name;
            company.case_implementation_contact_last_name = apiCase.Case_Implementation_Contact_Last_Name;
            company.case_id = apiCase.CaseId;
            company.sf_case_id = apiCase.SfCaseId;
            company.ImplementationPlans = apiCase.ImplementationPlanType;

            company.CompanyStatus = "Client Notified";

            company.CompanyProfileStatus = "Not Started";
            company.FundingStatus = "Not Started";
            company.BenefitStatus = "Not Started";
            company.CobraStatus = "Not Started";

            company.ReviewStatus = "No Review";
            company.BenefitReviewStatus = "No Review";
            company.CobraReviewStatus = "No Review";
            company.FundingReviewStatus = "No Review";


            company.CalendlyUserName = apiCase.Case_Implementation_Contact_Email;
            company.CreatedOn = DateTime.Now;

            company.SalesRepName = apiCase.SalesRepName;
            company.ClientAnnualRevenue = apiCase.ClientAnnualRevenue;
            company.LastRolloutError = apiCase.LastRolloutError;
            company.IsUsedForTesting = apiCase.IsUsedForTesting;
            company.IsReadyForProcessing = apiCase.IsReadyForProcessing;
            company.CrmUserId = apiCase.CrmUserId;

            var addedCompany = await companyRepository.Add(company);
            CompanyDetails companyDetails = new CompanyDetails()

            { Name = apiCase.LegalBusinessName, ImplementationPlanType = apiCase.ImplementationPlanType, ImplementationDate = apiCase.ImplementationDate, CreatedOn = DateTime.Now };
            //company.CreatedOn = DateTime.Now;
           

            companyDetails.CompanyId = addedCompany.Id;
            await companyDetailsRepository.Add(companyDetails);


            Address address = new Address();
            address.CreatedOn = DateTime.Now;
            address.CompanyId = addedCompany.Id;
            address.AddressLine1 = apiCase.StreetPhysicalAddress;
            address.State = apiCase.State;
            address.City = apiCase.City;
            address.Zip = apiCase.Zip;
            await addressRepository.Add(address);


            //foreach (Api_Case_Contact contact in case_Contacts)
            //{
            //    if (contact.ContactType == "Client" && contact.CaseId == apiCase.CaseId)
            //    {
            //        CompanyContact companyContact = new CompanyContact();
            //        companyContact.CompanyId = addedCompany.Id;
            //        companyContact.FirstName = contact.ContactFirstName;
            //        companyContact.LastName = contact.ContactLastName;
            //        companyContact.Email = contact.ContactEmail;
            //        companyContact.Phone = FormatPhoneNumber(contact.ContactPhone);
            //        companyContact.CaseContactId = contact.CaseContactId;
            //        companyContact.CaseId = contact.CaseId;
            //        companyContact.SFContactId = contact.SfContactId;
            //        companyContact.ContactIsImplementationContact = contact.ContactIsImplementationContact;
            //        //companyContact.Role = contact.ContactTitle;
            //        await companyContactRepository.Add(companyContact);
            //    }
            //    else if (contact.CaseId == apiCase.CaseId)
            //    {
            //        BrokerContact brokerContact = new BrokerContact();
            //        brokerContact.CompanyId = addedCompany.Id;
            //        brokerContact.BrokerageName = contact.OrganizationName;
            //        brokerContact.FirstName = contact.ContactFirstName;
            //        brokerContact.LastName = contact.ContactLastName;
            //        brokerContact.Email = contact.ContactEmail;
            //        brokerContact.Phone = FormatPhoneNumber(contact.ContactPhone);

            //        brokerContact.CaseContactId = contact.CaseContactId;
            //        brokerContact.CaseId = contact.CaseId;
            //        brokerContact.SFContactId = contact.SfContactId;
            //        brokerContact.ContactIsImplementationContact = contact.ContactIsImplementationContact;

            //        //brokerContact.Role = contact.ContactTitle;
            //        await brokerContactRepository.Add(brokerContact);
            //    }
            //}

            EmployeeBenefitsFileUpload employeeBenefitsFileUpload = new EmployeeBenefitsFileUpload();
            employeeBenefitsFileUpload.PlanName = "consumerBenefits";
            employeeBenefitsFileUpload.CompanyId = addedCompany.Id;
            await employeeBenefitsFileUploadRepository.Add(employeeBenefitsFileUpload);

            string[] fileCategories = {
                        "Summary of Benefits and Coverage (SBC) Documents",
                        "Medical Carrier Invoice",
                        "Age/Gender Banded Rates",
                        "COBRA Takeover File"
            };

            for (int i = 0; i < fileCategories.Length; i++)
            {
                CobraBenefit cobraBenefit = new CobraBenefit();
                cobraBenefit.FileId = employeeBenefitsFileUpload.Id;
                cobraBenefit.FileCategory = fileCategories[i];
                await cobraBenefitRepository.Add(cobraBenefit);
            }

            EmployeeBenefit employeeBenefit = new EmployeeBenefit();
            employeeBenefit.FileId = employeeBenefitsFileUpload.Id;
            employeeBenefit.FileCategory = "Employee Benefits File Upload";
            await employeeBenefitRepository.Add(employeeBenefit);

            EmployeeBenefitsFileUploadItem employeeBenefitsFileUploadItem = new EmployeeBenefitsFileUploadItem();
            employeeBenefitsFileUploadItem.EmployeeBenefitId = employeeBenefit.Id;
            employeeBenefitsFileUploadItem.FileCategory = "Employee Benefits File Upload";
            await ebFileUploadItemRepository.Add(employeeBenefitsFileUploadItem);



            string implementationPlans = apiCase.ImplementationPlanType;
            if (implementationPlans != null)
            {
                string[] plans = implementationPlans.Split(new char[] { ';', ',', '|' });


                if (plans.Contains("COBRA"))
                {
                    COBRAPlan cobraPlan = new COBRAPlan();
                    cobraPlan.IsOpenEnrollment = "false";
                    cobraPlan.CompanyId = addedCompany.Id;
                    await cobraPlanRepository.Add(cobraPlan);

                    CobraGeneralInformation cobraGeneralInformation = new CobraGeneralInformation();
                    cobraGeneralInformation.COBRAPlanId = cobraPlan.Id;
                    await cobraGeneralInformationRepository.Add(cobraGeneralInformation);

                    //COBRAMedicalPlan cobraMedicalPlan = new COBRAMedicalPlan();
                    //cobraMedicalPlan.IsDisabilityExtension = "false";
                    //cobraMedicalPlan.COBRAPlanId = cobraPlan.Id;
                    //await cobraMedicalPlanRepository.Add(cobraMedicalPlan);

                    //COBRADentalPlan cobraDentalPlan = new COBRADentalPlan();
                    //cobraDentalPlan.IsDisabilityExtension = "false";
                    //cobraDentalPlan.IsDivisionSpecific = "false";
                    //cobraDentalPlan.COBRAPlanId = cobraPlan.Id;
                    //await cobraDentalPlanRepository.Add(cobraDentalPlan);

                    //COBRAVisionPlan cobraVisionPlan = new COBRAVisionPlan();
                    //cobraVisionPlan.IsDisabilityExtension = "false";
                    //cobraVisionPlan.IsDivisionSpecific = "false";
                    //cobraVisionPlan.COBRAPlanId = cobraPlan.Id;
                    //await cobraVisionPlanRepository.Add(cobraVisionPlan);

                    //COBRAHRAPlan cobraHRAPlan = new COBRAHRAPlan();
                    //cobraHRAPlan.IsDisabilityExtension = "false";
                    //cobraHRAPlan.IsDivisionSpecific = "false";
                    //cobraHRAPlan.COBRAPlanId = cobraPlan.Id;
                    //await cobraHRAPlanRepository.Add(cobraHRAPlan);

                    //COBRAFSAPlan cobraFSAPlan = new COBRAFSAPlan();
                    //cobraFSAPlan.IsDisabilityExtension = "false";
                    //cobraFSAPlan.IsDivisionSpecific = "false";
                    //cobraFSAPlan.COBRAPlanId = cobraPlan.Id;
                    //await cobraFSAPlanRepository.Add(cobraFSAPlan);

                    //COBRAInsurancePlan cobraInsurancePlan = new COBRAInsurancePlan();
                    //cobraInsurancePlan.IsDisabilityExtension = "false";
                    //cobraInsurancePlan.IsDivisionSpecific = "false";
                    //cobraInsurancePlan.COBRAPlanId = cobraPlan.Id;
                    //await cobraInsurancePlanRepository.Add(cobraInsurancePlan);

                    //COBRAEAPPlan cobraEAPPlan = new COBRAEAPPlan();
                    //cobraEAPPlan.IsDisabilityExtension = "false";
                    //cobraEAPPlan.IsDivisionSpecific = "false";
                    //cobraEAPPlan.COBRAPlanId = cobraPlan.Id;
                    //await cobraEAPPlanRepository.Add(cobraEAPPlan);

                    //MedicalPlanCoverageRate medicalPlanCoverageRate = new MedicalPlanCoverageRate();
                    //medicalPlanCoverageRate.COBRAMedicalPlanId = cobraMedicalPlan.Id;
                    //await medicalPlanCoverageRateRepository.Add(medicalPlanCoverageRate);

                    //DentalCoverageRate dentalCoverageRate = new DentalCoverageRate();
                    //dentalCoverageRate.DentalPlanId = cobraDentalPlan.Id;
                    //await dentalCoverageRateRepository.Add(dentalCoverageRate);

                    //VisionCoverageRate visionCoverageRate = new VisionCoverageRate();
                    //visionCoverageRate.VisionPlanId = cobraVisionPlan.Id;
                    //await visionCoverageRateRepository.Add(visionCoverageRate);

                    //HRACoverageRate hraCoverageRate = new HRACoverageRate();
                    //hraCoverageRate.HRAPlanId = cobraHRAPlan.Id;
                    //await hraCoverageRateRepository.Add(hraCoverageRate);

                    //FSACoverageRate fsaCoverageRate = new FSACoverageRate();
                    //fsaCoverageRate.FSAPlanId = cobraFSAPlan.Id;
                    //await fsaCoverageRateRepository.Add(fsaCoverageRate);

                    //EAPCoverageRate eapCoverageRate = new EAPCoverageRate();
                    //eapCoverageRate.EAPPlanId = cobraEAPPlan.Id;
                    //await eapCoverageRateRepository.Add(eapCoverageRate);

                    //InsurancePlanCoverageRate insurancePlanCoverageRate = new InsurancePlanCoverageRate();
                    //insurancePlanCoverageRate.InsurancePlanId = cobraInsurancePlan.Id;
                    //await insurancePlanCoverageRateRepository.Add(insurancePlanCoverageRate);

                    ////Add another round of coverage list items 
                    //MedicalPlanCoverageRate medicalPlanCoverageRate2 = new MedicalPlanCoverageRate();
                    //medicalPlanCoverageRate2.COBRAMedicalPlanId = cobraMedicalPlan.Id;
                    //await medicalPlanCoverageRateRepository.Add(medicalPlanCoverageRate2);

                    //DentalCoverageRate dentalCoverageRate2 = new DentalCoverageRate();
                    //dentalCoverageRate2.DentalPlanId = cobraDentalPlan.Id;
                    //await dentalCoverageRateRepository.Add(dentalCoverageRate2);

                    //VisionCoverageRate visionCoverageRate2 = new VisionCoverageRate();
                    //visionCoverageRate2.VisionPlanId = cobraVisionPlan.Id;
                    //await visionCoverageRateRepository.Add(visionCoverageRate2);

                    //HRACoverageRate hraCoverageRate2 = new HRACoverageRate();
                    //hraCoverageRate2.HRAPlanId = cobraHRAPlan.Id;
                    //await hraCoverageRateRepository.Add(hraCoverageRate2);

                    //FSACoverageRate fsaCoverageRate2 = new FSACoverageRate();
                    //fsaCoverageRate2.FSAPlanId = cobraFSAPlan.Id;
                    //await fsaCoverageRateRepository.Add(fsaCoverageRate2);

                    //EAPCoverageRate eapCoverageRate2 = new EAPCoverageRate();
                    //eapCoverageRate2.EAPPlanId = cobraEAPPlan.Id;
                    //await eapCoverageRateRepository.Add(eapCoverageRate2);

                    //InsurancePlanCoverageRate insurancePlanCoverageRate2 = new InsurancePlanCoverageRate();
                    //insurancePlanCoverageRate2.InsurancePlanId = cobraInsurancePlan.Id;
                    //await insurancePlanCoverageRateRepository.Add(insurancePlanCoverageRate2);

                    EnrollmentAndEligibilityContact enrollmentAndEligibilityContact = new EnrollmentAndEligibilityContact();
                    enrollmentAndEligibilityContact.ContactType = "clarity";
                    enrollmentAndEligibilityContact.COBRAPlanId = cobraPlan.Id;
                    await enrollmentAndEligibilityContactRepository.Add(enrollmentAndEligibilityContact);

                    CobraOpenEnrollmentManagement cobraOpenEnrollmentManagement = new CobraOpenEnrollmentManagement();
                    cobraOpenEnrollmentManagement.COBRAPlanId = cobraPlan.Id;
                    await cobraOpenEnrollmentManagementRepository.Add(cobraOpenEnrollmentManagement);
                    //COBRAClientContact cobraClientContact = new COBRAClientContact();
                    //cobraClientContact.EnrollmentAndEligibilityContactId = enrollmentAndEligibilityContact.Id;
                    //await cobraClientContactRepository.Add(cobraClientContact);

                    //COBRABrokerContact cobraBrokerContact = new COBRABrokerContact();
                    //cobraBrokerContact.EnrollmentAndEligibilityContactId = enrollmentAndEligibilityContact.Id;
                    //await cobraBrokerContactRepository.Add(cobraBrokerContact);

                    COBRAFunding cobraFunding = new COBRAFunding();
                    cobraFunding.COBRAPremiumProvider = "Employer";
                    cobraFunding.IsBrokerorPartnerPayment = "false";
                    cobraFunding.IsCreditCobraPremiumRemittance = "true";
                    cobraFunding.IsDebitMonthlyAdministrationFee = "true";
                    cobraFunding.CompanyId = addedCompany.Id;
                    await cobraFundingRepository.Add(cobraFunding);
                }
                if (plans.Contains("HRA") || plans.Contains("FSA") || plans.Contains("DCA") || plans.Contains("LPFSA") || plans.Contains("HSA") || (plans.Contains("Parking") && plans.Contains("Transit")))
                {
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

                    PlanType planType = new PlanType();
                    planType.EmployeeBenefitsPlanId = employeeBenefitsPlan.Id;
                    await planTypeRepository.Add(planType);

                    PayScheduleType payScheduleType = new PayScheduleType();
                    payScheduleType.EmployeeBenefitsPlanId = employeeBenefitsPlan.Id;
                    await payScheduleTypeRepository.Add(payScheduleType);

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

                    if (plans.Contains("HRA"))
                    {
                        EmployeeBenefitsHRA employeeBenefitsHRA = new EmployeeBenefitsHRA();
                        employeeBenefitsHRA.ReimbursableExpenses = "0";
                        employeeBenefitsHRA.HRAUnusedFund = "Forfeited";
                        employeeBenefitsHRA.IsProRated = "false";
                        employeeBenefitsHRA.IsEmployeeResponsible = "false";
                        employeeBenefitsHRA.IsHRAEmployeeResponsible = "false";
                        employeeBenefitsHRA.IsParticipantResponsible = "false";
                        employeeBenefitsHRA.IsHRAorFSA = "HRA";
                        employeeBenefitsHRA.IsPlanForClarityBenefitCard = "false";
                        employeeBenefitsHRA.IsDependentCard = "true";
                        employeeBenefitsHRA.HRAType = "Standard Integrated";
                        employeeBenefitsHRA.ActiveEmployeeRunoutPeriod = "90 Days";
                        employeeBenefitsHRA.TerminatedEmployeeRunoutPeriod = "90 Days";
                        employeeBenefitsHRA.IsDependentCardOption = "Spouse Only";
                        employeeBenefitsHRA.EmployeeBenefitsPlanId = employeeBenefitsPlan.Id;
                        await employeeBenefitsHRARepository.Add(employeeBenefitsHRA);
                    }
                    if (plans.Contains("FSA") || plans.Contains("DCA") || plans.Contains("LPFSA"))
                    {
                        EmployeeBenefitsFSA employeeBenefitsFSA = new EmployeeBenefitsFSA();

                        if (plans.Contains("HSA"))
                        {
                            employeeBenefitsFSA.IsHSA = "true";
                            employeeBenefitsFSA.IsOfferLPFSA = "true";
                        }
                        else
                        {
                            employeeBenefitsFSA.IsHSA = "false";
                            if (plans.Contains("LPFSA"))
                            {
                                employeeBenefitsFSA.IsOfferLPFSA = "true";
                            }
                            else
                            {
                                employeeBenefitsFSA.IsOfferLPFSA = "false";
                            }
                        }
                           
                        employeeBenefitsFSA.IsFSAChecked = "true";
                        employeeBenefitsFSA.IsStandardPlan = "true";
                        employeeBenefitsFSA.AllowCarryOver = "true";
                        employeeBenefitsFSA.RunOutPeriod = "Planning Period";
                        employeeBenefitsFSA.AllowGracePeriod = "false";
                        employeeBenefitsFSA.MinAnnualElectionAmount = "0";
                        employeeBenefitsFSA.MaxAnnualElectionAmount = "3200";
                        employeeBenefitsFSA.EmployerContribution = "0";
                        employeeBenefitsFSA.ActiveEmployeeRunoutPeriod = "90 Days";
                        employeeBenefitsFSA.TerminatedEmployeeRunoutPeriod = "90 Days";
                        employeeBenefitsFSA.TerminatedEmployeeCoverageEndPeriod = "Date of Termination";
                        employeeBenefitsFSA.EmployeeBenefitsPlanId = employeeBenefitsPlan.Id;
                        await employeeBenefitsFSARepository.Add(employeeBenefitsFSA);

                        EmployeeBenefitsLPFSA employeeBenefitsLPFSA = new EmployeeBenefitsLPFSA();
                        employeeBenefitsLPFSA.IsLPFSAChecked = "true";
                        employeeBenefitsLPFSA.IsStandardPlan = "true";
                        employeeBenefitsLPFSA.AllowCarryOver = "true";
                        employeeBenefitsLPFSA.AllowGracePeriod = "false";
                        employeeBenefitsLPFSA.ActiveEmployeeRunoutPeriod = "90 Days";
                        employeeBenefitsLPFSA.TerminatedEmployeeRunoutPeriod = "90 Days";
                        employeeBenefitsLPFSA.RunOutPeriod = "Planning Period";
                        employeeBenefitsLPFSA.MinAnnualElectionAmount = "0";
                        employeeBenefitsLPFSA.MaxAnnualElectionAmount = "3200";
                        employeeBenefitsLPFSA.EmployerContribution = "0";
                        employeeBenefitsLPFSA.TerminatedEmployeeCoverageEndPeriod = "Date of Termination";
                        employeeBenefitsLPFSA.EmployeeBenefitsFSAId = employeeBenefitsFSA.Id;
                        await employeeBenefitsLPFSARepository.Add(employeeBenefitsLPFSA);

                        EmployeeBenefitsDCA employeeBenefitsDCA = new EmployeeBenefitsDCA();
                        employeeBenefitsDCA.IsDCAChecked = "true";
                        employeeBenefitsDCA.IsStandardPlan = "true";
                        employeeBenefitsDCA.TotalDays = 75;
                        employeeBenefitsDCA.IsDCAChecked = "true";
                        employeeBenefitsDCA.AllowGracePeriod = "true";
                        employeeBenefitsDCA.ActiveEmployeeRunoutPeriod = "90 Days";
                        employeeBenefitsDCA.TerminatedEmployeeRunoutPeriod = "90 Days";
                        employeeBenefitsDCA.RunOutPeriod = "Planning Period";
                        employeeBenefitsDCA.MinAnnualElectionAmount = "0";
                        employeeBenefitsDCA.MaxAnnualElectionAmount = "5000";
                        employeeBenefitsDCA.EmployerContribution = "0";
                        employeeBenefitsDCA.TerminatedEmployeeCoverageEndPeriod = "End of Plan Year";
                        employeeBenefitsDCA.EmployeeBenefitsFSAId = employeeBenefitsFSA.Id;
                        await employeeBenefitsDCARepository.Add(employeeBenefitsDCA);
                    }



                    if (plans.Contains("HSA"))
                    {
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
                    }
                    if (plans.Contains("Parking") && plans.Contains("Transit"))
                    {
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
                    }

                }
            }

            return "Success";


        }
        private string FormatPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return string.Empty;
            }
            phoneNumber = new string(phoneNumber.Where(char.IsDigit).ToArray());

            if (phoneNumber.Length < 10)
            {
                return phoneNumber;
            }

            if (phoneNumber.Length > 10)
            {
                phoneNumber = phoneNumber.Substring(phoneNumber.Length - 10);
            }

            return string.Format("({0}) {1} {2}",
                phoneNumber.Substring(0, 3),
                phoneNumber.Substring(3, 3),
                phoneNumber.Substring(6));
        }

    }
}
