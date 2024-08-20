using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace ClarityImplementation.API.Models
{
    public class AffiliatedCompanyDivisionFundingFile
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string UploadedFileURL { get; set; }
        public int AffiliatedCompanyDivisionId { get; set; }
        [JsonIgnore]
        [AllowNull]
        public AffiliatedCompanyDivision AffiliatedCompanyDivision { get; set; }
    }
}
