using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace ClarityImplementation.API.Models.Plans
{
    public class PlanType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Plan { get; set; }
        [NotMapped]
        public List<string> SelectedPlans { get; set; }
        [NotMapped]
        public List<string> AvailablePlans { get; set; }
        public string Group { get; set; }

        [NotMapped]
        public List<string> SelectedGroups { get; set; }
        
        public string EligibilityDefinition { get; set; }
        public int EmployeeBenefitsPlanId { get; set; }
        public string OtherOptionEligibilityDefinition { get; set; }

        [JsonIgnore]
        [AllowNull]
        public EmployeeBenefitsPlan EmployeeBenefitsPlan { get; set; }
    }
}
