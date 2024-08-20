using ClarityImplementation.API.Models.Funding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace ClarityImplementation.API.Models.FileUpload
{
    public class EmployeeBenefit
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string FileCategory { get; set; }
        public string FileUrl { get; set; }
        public List<EmployeeBenefitsFileUploadItem> EmployeeBenefitsFileUploadItems { get; set; }
        public int FileId { get; set; }
        [JsonIgnore]
        [AllowNull]
        public EmployeeBenefitsFileUpload EmployeeBenefitsFileUpload { get; set; }
    }
}
