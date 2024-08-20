using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace ClarityImplementation.API.Models.FileUpload
{
    public class CobraFileUpload
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string FileCategory { get; set; }
        public string UploadedFileUrl { get; set; }
        public int CobraBenefitId { get; set; }
        [JsonIgnore]
        [AllowNull]
        public CobraBenefit CobraBenefit { get; set; }
    }
}
