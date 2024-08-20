using ClarityImplementation.API.Models.Funding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace ClarityImplementation.API.Models.FileUpload
{
    public class EmployeeBenefitsFileUploadItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string FileCategory { get; set; }
        public string FileUrl { get; set; }
        public int EmployeeBenefitId { get; set; }
        [JsonIgnore]
        [AllowNull]
        public EmployeeBenefit EmployeeBenefit { get; set; }
    }
}
