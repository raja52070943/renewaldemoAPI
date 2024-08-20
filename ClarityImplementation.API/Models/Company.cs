using ClarityImplementation.API.Models.Event;
using ClarityImplementation.API.Models.FileUpload;
using ClarityImplementation.API.Models.Funding;
using ClarityImplementation.API.Models.Plans;
using ClarityImplementation.API.Models.Plans.COBRA;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace ClarityImplementation.API.Models
{
    public class Company
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string CompanyTotalStatus { set; get; }
        public string CompanyStatus { set; get; }
        public CompanyDetails CompanyDetails { set; get; }
        public Address Address { get; set; }
        public string IsCompanyDivision { get; set; }
        public List<CompanyDivision> CompanyDivisions { get; set; }
        public string IsAffiliatedCompany { get; set; }
        public List<AffiliatedCompany> AffiliatedCompanies { get; set; }

        //public List<CompanyContact> CompanyContacts { get; set; }
        //public List<BrokerContact> BrokerContacts { get; set; }

        public List<Api_Case_Contact> Api_Case_Contacts { get; set; }
        public EmployeeBenefitsPlan EmployeeBenefitsPlan { get; set; }
        public COBRAPlan COBRAPlan { get; set; }
        public COBRAFunding COBRAFunding { get; set; }
        public EmployeeBenefitsFunding EmployeeBenefitsFunding { get; set; }
        public EmployeeBenefitsFileUpload EmployeeBenefitsFileUpload { get; set; }
        public string KickoffStatus { get; set; }

        public string KickoffUri { get; set; }

        public string CalendlyUserName { set; get; }
        public string CaseOwnerEmail { set; get; }
        public string CaseOwnerName { set; get; }
        public string case_id { set; get; }
        public string sf_case_id { set; get; }
        public string case_implementation_contact_email { set; get; }
        public string case_implementation_contact_first_name { set; get; }
        public string case_implementation_contact_last_name { set; get; }

        public string NotificationDate { get; set; }
        [Range(0, 100)]
        public int Progress { get; set; }

        [Range(0, 100)]
        public int TotalProgress { get; set; }

        [Range(0, 100)]
        public int TotalPlanProgress { get; set; }

        [Range(0, 100)]
        public int PendingProgress { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedOn { get; set; }

        public string CompanyProfileStatus { get; set; }
        public string FundingStatus { get; set; }
        public string BenefitStatus {  get; set; }
        public string CobraStatus {  get; set; }
        public string Status { get; set; }
        public string IntroStatus {  get; set; }
        public string ReviewStatus { get; set; }
        public string BenefitReviewStatus { get; set; }
        public string CobraReviewStatus { get; set; }
        public string FundingReviewStatus { get; set; }




        public string Reminder1 {  get; set; }
        public DateTime Reminder1SentDate { get; set; }
        public string Reminder2 { get; set; }

        public DateTime Reminder2SentDate { get; set; }
        public string Reminder3 { get; set; }

        public DateTime Reminder3SentDate { get; set; }

        public string UploadEnrollEmail { get; set; }
        public DateTime UploadEnrollEmailDate { get; set; }
        public string CompletedEmail { get; set; }
        public string UploadEnrollReminder1 { get; set; }
        public DateTime UploadEnrollReminder1Date { get; set; }
        public string UploadEnrollReminder2 { get; set; }
        public DateTime UploadEnrollReminder2Date { get; set; }
        public string UploadEnrollReminder3 { get; set; }
        public DateTime UploadEnrollReminder3Date { get; set; }
        public string IsEmailSent { get; set; }
        [DataType(DataType.Date)]
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }

        public CalendlyEvent CalendlyEvent { get; set; }

        public DateTime StatusUpdateDateTime { get; set; }

        
        public string SalesRepName { get; set; }

        public int? ClientAnnualRevenue { get; set; }

        public string LastRolloutError { get; set; }

        public bool? IsUsedForTesting { get; set; } = false;

        public bool? IsReadyForProcessing { get; set; } = false;

        public string AdminStatus { get; set; }

        public string? CrmUserId { get; set; }


        public int? TotalFundingProgress { get; set; }
        public string? ImplementationPlans { get; set; }

    }
}
