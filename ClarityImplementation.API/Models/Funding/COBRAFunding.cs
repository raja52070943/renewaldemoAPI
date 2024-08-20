using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace ClarityImplementation.API.Models.Funding
{
    public class COBRAFunding
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [DefaultValue("false")]
        public string IsRequiredSeparateRemittance { get; set; }
        [DefaultValue("true")]
        public string IsCreditCobraPremiumRemittance { get; set; }
        [DefaultValue("true")]
        public string IsDebitMonthlyAdministrationFee { get; set; }
        [DefaultValue("false")]
        public string IsBrokerorPartnerPayment { get; set; }
        public string BankAccountName { get; set; }
        
        public string BankAccountNumber { get; set; }
        public string BankRoutingNumber { get; set; }

        public string BankAccountNameForCreditCobra { get; set; }
        
        public string BankAccountNumberForCreditCobra { get; set; }
        public string BankRoutingNumberForCreditCobra { get; set; }

        public string BankAccountNameForAdminFee { get; set; }
        
        public string BankAccountNumberForAdminFee { get; set; }
        public string BankRoutingNumberForAdminFee { get; set; }

       
        public string CobraDefaultFundingFile { get; set; }
        [DefaultValue("true")]
        public string COBRAPremiumProvider { get; set; }
        public string CarrierFirstName { get; set; }
        public string CarrierLastName { get; set; }
        public string CarrierEmail { get; set; }
        public string BrokerFirstName { get; set; }
        public string BrokerLastName { get; set; }
        public string BrokerEmail { get; set; }
        public string CobraDocument { get; set; }
        public string IsCobraACHFormChecked { get; set; }
        public List<COBRAFundingFile> COBRAFundingFiles { get; set; }

        public string CobraSignatureName { get; set; }
        public string CobraSignatureTitle { get; set; }
        public string CobraSignatureDate { get; set; }


        [Range(0, 100)]
        public int Progress { get; set; }
        [Range(0, 100)]
        public int TotalFundingProgress { get; set; }
        public int CompanyId { get; set; }
        [JsonIgnore]
        [AllowNull]
        public Company Company { get; set; }
    }
}
