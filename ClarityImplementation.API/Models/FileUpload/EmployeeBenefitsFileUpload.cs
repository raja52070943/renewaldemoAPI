using ClarityImplementation.API.Models.Plans.COBRA;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace ClarityImplementation.API.Models.FileUpload
{
    public class EmployeeBenefitsFileUpload
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string PlanName { get; set; }
        public string IsEnrollmentTemplate { get; set; }
        [Range(0, 100)]
        public int Progress { get; set; }
        public List<CobraBenefit> CobraBenefits { get; set; }
        public List<EmployeeBenefit> EmployeeBenefits { get; set; }
        public int CompanyId { get; set; }
        [JsonIgnore]
        [AllowNull]
        public Company Company { get; set; }
    }
}
