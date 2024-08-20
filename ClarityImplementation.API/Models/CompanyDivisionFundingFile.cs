using ClarityImplementation.API.Models.Funding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace ClarityImplementation.API.Models
{
    public class CompanyDivisionFundingFile
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string UploadedFileURL { get; set; }
        public int CompanyDivisionId { get; set; }
        [JsonIgnore]
        [AllowNull]
        public CompanyDivision CompanyDivision { get; set; }
    }
}
