using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace ClarityImplementation.API.Models
{
    public class CompanyDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string DBA { get; set; }
        public string EINNumber { get; set; }
        [DisplayName("Products")]
        public string ImplementationPlanType { get; set; }
        public string IncorporationState { get; set; }
        public string IncorporationDate { get; set; }
        public string EmployerEntityType { get; set; }
        public string EligibleEmployees { set; get; }
        public DateTime ImplementationDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CompanyId { get; set; }
        [JsonIgnore]
        [AllowNull]
        public Company Company { get; set; }
    }
}
