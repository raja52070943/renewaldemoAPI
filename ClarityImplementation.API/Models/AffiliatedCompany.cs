using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace ClarityImplementation.API.Models
{
    public class AffiliatedCompany
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string AffiliateName { get; set; }
        public string EmployerEntityType { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        //[DefaultValue(false)]
        //public bool IsEINNumber { get; set; }
        public string EINNumber { get; set; }
        [DefaultValue(false)]
        public string IsCompanyDivision { get; set; }
        public List<AffiliatedCompanyDivision> AffiliatedCompanyDivisions { get; set; }

        public List<AffiliatedCompanyFundingFile> AffiliatedCompanyFundingFiles { get; set; }


        [DataType(DataType.Date)]
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public string EmployeeBenefitsFundingFile { get; set; }
        public string COBRAFundingFile { get; set; }
        public string BankAccountName { get; set; }
        public string BankAccountNumber { get; set; }
        public string BankRoutingNumber { get; set; }

        [DefaultValue("true")]
        public string IsDebitConsumerBenefitFundingForClient { get; set; }
        [DefaultValue("true")]
        public string IsDebitMonthlyAdministrationFeeForClient { get; set; }
        [DefaultValue("false")]
        public string IsBrokerorPartnerPaymentForClient { get; set; }
        [DefaultValue("true")]
        public string IsDebitConsumerBenefitFunding { get; set; }
        [DefaultValue("true")]
        public string IsCreditCobraPremiumRemittance { get; set; }
        [DefaultValue("true")]
        public string IsDebitMonthlyAdministrationFee { get; set; }
        [DefaultValue("false")]
        public string IsBrokerorPartnerPayment { get; set; }
        public string BankAccountNameForCreditCobra { get; set; }
        public string BankAccountNumberForCreditCobra { get; set; }
        public string BankRoutingNumberForCreditCobra { get; set; }
        public string BankAccountNameForAdminFee { get; set; }
        public string BankAccountNumberForAdminFee { get; set; }
        public string BankRoutingNumberForAdminFee { get; set; }

        public int CompanyId { get; set; }
        [JsonIgnore]
        [AllowNull]
        public Company Company { get; set; }
    }
}
