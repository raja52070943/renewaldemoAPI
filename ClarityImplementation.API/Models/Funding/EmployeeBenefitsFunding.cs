using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace ClarityImplementation.API.Models.Funding
{
    public class EmployeeBenefitsFunding
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [DefaultValue("false")]
        public string IsClarityRequiredSeparateAccount { get; set; }
        [DefaultValue("false")]
        public string IsClientRequiredSeparateAccount { get; set; }
        public string FundingModel { get; set; }
        public string FundingDocument {  get; set; }
        public string OwnAccountFundingDocument { get; set; }
        public string IsACHFormChecked { get; set; }
        public List<EmployeeBenefitsFundingFile> EmployeeBenefitsFundingFiles { get; set; }
        [Range(0, 100)]
        public int Progress { get; set; }
        [Range(0, 100)]
        public int TotalFundingProgress { get; set; }
        public string BankAccountName { get; set; }
        
        public string BankAccountNumber { get; set; }
        public string BankRoutingNumber { get; set; }
        public string BankAccountNameForBenefitFunding { get; set; }
        
        public string BankAccountNumberForBenefitFunding { get; set; }
        public string BankRoutingNumberForBenefitFunding { get; set; }
        public string BankAccountNameForClarity { get; set; }
        
        public string BankAccountNumberForClarity { get; set; }
        public string BankRoutingNumberForClarity { get; set; }
        
        [DefaultValue("true")]
        public string IsDebitConsumerBenefitFundingForClarity { get; set; }
        [DefaultValue("true")]
        public string IsDebitMonthlyAdministrationFeeForClarity { get; set; }
        [DefaultValue("false")]
        public string IsBrokerorPartnerPaymentForClarity { get; set; }
        [DefaultValue("true")]
        public string IsDebitConsumerBenefitFundingForClient { get; set; }
        [DefaultValue("true")]
        public string IsDebitMonthlyAdministrationFeeForClient { get; set; }
        [DefaultValue("false")]
        public string IsBrokerorPartnerPaymentForClient { get; set; }
        [DefaultValue("true")]
        public string IsDebitConsumerBenefitFunding { get; set; }
        [DefaultValue("true")]
        public string IsDebitMonthlyAdministrationFee { get; set; }
        [DefaultValue("false")]
        public string IsBrokerorPartnerPayment { get; set; }
        public string SignatureName { get; set; }
        public string SignatureTitle { get; set; }
        public string SignatureDate { get; set; }
        public string ClientFundingFile { get; set; }
        public int CompanyId { get; set; }
        [JsonIgnore]
        [AllowNull]
        public Company Company { get; set; }

    }
}
