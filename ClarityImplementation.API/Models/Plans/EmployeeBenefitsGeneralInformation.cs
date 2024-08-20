using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace ClarityImplementation.API.Models.Plans
{
    public class EmployeeBenefitsGeneralInformation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string InformationProvider { get; set; }
        public string InformationProviderVendorName { get; set; }
        public string InformationProviderVendorContactFirstName { get; set; }
        public string InformationProviderVendorContactLastName { get; set; }
        public string InformationProviderVendorContactEmail { get; set; }
        public string PayrollProvider { get; set; }
        public string PayrollProviderVendorName { get; set; }
        public string PayrollProviderOtherVendorName { get; set; }
        public string PayrollProviderFirstName { get; set; }
        public string PayrollProviderLastName { get; set; }
        public string PayrollProviderContactEmail { get; set; }
        public string AutopostOption { get; set; }
        public string ContributionManagerOption { get; set; }
        public string ClientDepositOption { get; set; }
        public string VendorDepositOption { get; set; }
        public int EmployeeBenefitsPlanId { get; set; }
        [JsonIgnore]
        [AllowNull]
        public EmployeeBenefitsPlan EmployeeBenefitsPlan { get; set; }
    }
}
