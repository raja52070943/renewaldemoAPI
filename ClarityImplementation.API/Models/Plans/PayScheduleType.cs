using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace ClarityImplementation.API.Models.Plans
{
    public class PayScheduleType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string PayScheduleFrequency { get; set; }
        public string FirstPaycheckDate { get; set; }
        public int EmployeeBenefitsPlanId { get; set; }

        [JsonIgnore]
        [AllowNull]
        public EmployeeBenefitsPlan EmployeeBenefitsPlan { get; set; }
    }
}
