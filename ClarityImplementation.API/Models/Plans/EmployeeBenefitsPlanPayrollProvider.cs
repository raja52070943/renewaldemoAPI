using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace ClarityImplementation.API.Models.Plans
{
    public class EmployeeBenefitsPlanPayrollProvider
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string ProviderName { get; set; }
        public bool IsVendorInformation { get; set; }
        public string VendorName { get; set; }

        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public string ContactEmail { get; set; }

        public int EmployeeBenefitsPlanId { get; set; }
        [JsonIgnore]
        [AllowNull]
        public EmployeeBenefitsPlan EmployeeBenefitsPlan { get; set; }
    }
}
