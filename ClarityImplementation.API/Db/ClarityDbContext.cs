using ClarityImplementation.API.Models;

using ClarityImplementation.API.Models.Communication;
using ClarityImplementation.API.Models.Event;
using ClarityImplementation.API.Models.FileUpload;
using ClarityImplementation.API.Models.Funding;
using ClarityImplementation.API.Models.Kickoff;
using ClarityImplementation.API.Models.Plans;
using ClarityImplementation.API.Models.Plans.COBRA;
using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace ClarityImplementation.API.Db
{
    public class ClarityDbContext : DbContext
    {
        public ClarityDbContext(DbContextOptions<ClarityDbContext> options) : base(options) {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>()
                .Property(e => e.CreatedOn)
                .HasDefaultValue(DateTime.Now);
            modelBuilder.Entity<Company>()
           .ToTable(tb => tb.HasTrigger("trg_UpdateCompanyStatusField"));
            modelBuilder.Entity<Company>()
           .ToTable(tb => tb.HasTrigger("trg_UpdateCobraStatusField"));
            modelBuilder.Entity<Company>()
           .ToTable(tb => tb.HasTrigger("trg_UpdateEBStatusField"));
            modelBuilder.Entity<Company>()
           .ToTable(tb => tb.HasTrigger("trg_UpdateFundingStatusField"));
        }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<AffiliatedCompany> AffiliatedCompanies { get; set; }
        public DbSet<BrokerContact> BrokerContacts { get; set; }
        public DbSet<CompanyContact> CompanyContacts { get; set; }
        public DbSet<CompanyDivision> CompanyDivisions { get; set; }
        public DbSet<CompanyDivisionFundingFile> CompanyDivisionFundingFiles { get; set; }
        public DbSet<AffiliatedCompanyDivisionFundingFile> AffiliatedCompanyDivisionFundingFiles { get; set; }
        public DbSet<AffiliatedCompanyFundingFile> AffiliatedCompanyFundingFiles { get; set; }
        public DbSet<CobraFileUpload> CobraFileUploads { get; set; }


        public DbSet<AffiliatedCompanyDivision> AffiliatedCompanyDivisions { get; set; }
        public DbSet<ContactResponsibility> ContactResponsibilities { get; set; }
        public DbSet<ContactRole> ContactRoles { get; set; }
        public DbSet<BrokerRole> BrokerRoles { get; set; }

        public DbSet<CompanyStatus> CompanyStatuses { get; set; }
        public DbSet<EmployerEntityType> EmployerEntityTypes { get; set; }
        public DbSet<IncorporationState> IncorporationStates { get; set; }
        public DbSet<Page> Pages { set; get; }
        public DbSet<PageMetaDataField> PageMetaDataFields { set; get; }
        public DbSet<Api_Case> Api_Cases { get; set; }
        public DbSet<Api_File_Upload> Api_File_Uploads { get; set; }
        public DbSet<Api_Notification_Log> Api_Notification_Logs { get; set; }

        public DbSet<KickoffUserData> KickoffUserDatas { get; set; }

        public DbSet<Api_Case_Contact> Api_Case_Contacts { get; set; }
        public DbSet<EmployeeBenefitsPlan> EmployeeBenefitsPlans {get;set;}
        public DbSet<EmployeeBenefitsHSA> EmployeeBenefitsHSAs { get; set; }
        public DbSet<EmployeeBenefitsHRA> EmployeeBenefitsHRAs { get; set; }
        public DbSet<EmployeeBenefitsFSA> EmployeeBenefitsFSAs { get; set; }
        public DbSet<EmployeeBenefitsSmartRide> EmployeeBenefitsSmartRides { get; set; }
        public DbSet<EmployeeBenefitsEnrollment> EmployeeBenefitsEnrollments { get; set; }
        public DbSet<EmployeeBenefitsDCA> EmployeeBenefitsDCAs { get; set; }
        public DbSet<EmployeeBenefitsLPFSA> EmployeeBenefitsLPFSAs { get; set; }
        public DbSet<EBPlan> EBPlans { get; set; }

        public DbSet<Email> Emails { get; set; }
        public DbSet<EmployeeBenefitsFundingFile> EmployeeBenefitsFundingFiles { get; set; }
        public DbSet<COBRAFundingFile> COBRAFundingFiles { get; set; }
        public DbSet<InformationProvider> InformationProviders { get; set; }
        public DbSet<TerminatedEmployeeRunoutPeriod> TerminatedEmployeeRunoutPeriods { get; set; }
        public DbSet<ActiveEmployeeRunoutPeriod> ActiveEmployeeRunoutPeriods { get; set; }
        public DbSet<PayrollProvider> PayrollProviders { get; set; }
        public DbSet<PayScheduleFrequency> PayScheduleFrequencies { get; set; }
        public DbSet<EmployeeContributionGroup> EmployerContributionGroups { get; set; }
        public DbSet<EnrolledEmployee> EnrolledEmployees { get; set; }
        public DbSet<PlanType> PlanTypes { get; set; }
        public DbSet<EligibilityDefinition> EligibilityDefinitions { get; set; }
        public DbSet<BenefitPlan> BenefitPlans { get; set; }
        public DbSet<EmployeeGroup> EmployeeGroups { get; set; }
        public DbSet<TerminatedEmployeeCoverageEndPeriod> TerminatedEmployeeCoverageEndPeriods { get; set; }
        public DbSet<RunoutPeriod> RunoutPeriods { get; set; }
        public DbSet<MidYearPlan> MidYearPlans { get; set; }
        public DbSet<PriorYearPlan> PriorYearPlans { get; set; }
        public DbSet<DataTransmissionMethod> DataTransmissionMethods { get; set; }
        public DbSet<COBRAPlan> COBRAPlans { get; set; }     
        public DbSet<COBRADentalPlan> COBRADentalPlans { get; set; }
        public DbSet<COBRAFSAPlan> COBRAFSAPlans { get; set; }
        public DbSet<COBRAHRAPlan> COBRAHRAPlans { get; set; }
        public DbSet<COBRAInsurancePlan> COBRAInsurancePlans { get; set; }
        public DbSet<COBRAMedicalPlan> COBRAMedicalPlans { get; set; }       
        public DbSet<COBRAVisionPlan> COBRAVisionPlans { get; set; }
        public DbSet<DentalCoverageRate> DentalCoverageRates { get; set; }
        public DbSet<FSACoverageRate> FSACoverageRates { get; set; }
        public DbSet<HRACoverageRate> HRACoverageRates { get; set; }
        public DbSet<InsurancePlanCoverageRate> InsurancePlanCoverageRates { get; set; }
        public DbSet<MedicalPlanCoverageRate> MedicalPlanCoverageRates { get; set; }
        public DbSet<VisionCoverageRate> VisionCoverageRates { get; set; }
        public DbSet<EmployeeBenefitsFunding> EmployeeBenefitsFundings { get; set; }
        public DbSet<COBRAFunding> COBRAFundings { get; set; }
        public DbSet<EnrollmentAndEligibilityContact> EnrollmentAndEligibilityContacts { get; set; }
        public DbSet<COBRABrokerContact> COBRABrokerContacts { get; set; }
        public DbSet<COBRAClientContact> COBRAClientContacts { get; set; }
        public DbSet<AccessEmployeeCommunication> AccessEmployeeCommunications { get; set; }
        public DbSet<EmployeeBenefitsFileUpload> EmployeeBenefitsFileUploads { get; set; }
        public DbSet<CobraBenefit> CobraBenefits {  get; set; }
        public DbSet<EmployeeBenefit> EmployeeBenefits { get; set; }
        public DbSet<HRAReimbursableExpenses> HRAReimbursableExpenses { get; set; }
        public DbSet<PayrollVendorName> PayrollVendorNames { get; set; }
        public DbSet<COBRAEAPPlan> COBRAEAPPlans { get; set; }
        public DbSet<EAPCoverageRate> EAPCoverageRates { get; set; }
        public DbSet<BenefitPlanType> BenefitPlanTypes { get; set; }
        public DbSet<CompanyDetails> CompanyDetails { get; set; }
        public DbSet<CobraGeneralInformation> CobraGeneralInformations { get; set; }
        public DbSet<EmployeeBenefitsGeneralInformation> EmployeeBenefitsGeneralInformations { get; set; }
        public DbSet<PayScheduleType> PayScheduleTypes { get; set; }
        public DbSet<CalendlyEvent> CalendlyEvents { get; set; }
        public DbSet<EmployeeBenefitsFileUploadItem> EmployeeBenefitsFileUploadItems { get; set; }
        public DbSet<ApiCaseContactChangeRequest> ApiCaseContactChangeRequests { get; set; }
        public DbSet<CobraOpenEnrollmentManagement> cobraOpenEnrollmentManagements { get; set; }

    }
}
