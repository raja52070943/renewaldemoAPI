using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace ClarityImplementation.API.Models.Funding
{
    public class EmployeeBenefitsFundingFile
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string FundingModel { get; set; }
        public string UploadedFileURL { get; set; }
        public int EmployeeBenefitsFundingId { get; set; }
        [JsonIgnore]
        [AllowNull]
        public EmployeeBenefitsFunding EmployeeBenefitsFunding { get; set; }

    }
}
