using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace ClarityImplementation.API.Models.Plans
{
    public class EmployeeContributionGroup
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string EligibilityContigent { get; set; }
        public string SingleContribution { get; set; }
        public string EmployeeSpouseContribution { get; set; }
        public string EmployeeChildContribution { get; set; }
        public string FamilyContribution { get; set; }
        public int EmployeeBenefitsHSAId { get; set; }
        [JsonIgnore]
        [AllowNull]
        public EmployeeBenefitsHSA EmployeeBenefitsHSA { get; set; }
    }
}
