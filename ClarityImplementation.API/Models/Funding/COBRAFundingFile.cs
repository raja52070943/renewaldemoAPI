using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace ClarityImplementation.API.Models.Funding
{
    public class COBRAFundingFile
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string CobraPremiumProvider { get; set; }
        public string CobraUploadedFileURL { get; set; }
        public int CobraFundingId { get; set; }
        [JsonIgnore]
        [AllowNull]
        public COBRAFunding COBRAFunding { get; set; }
    }
}
