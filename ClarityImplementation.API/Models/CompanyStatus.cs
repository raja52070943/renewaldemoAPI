using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace ClarityImplementation.API.Models
{
    public class CompanyStatus
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string StatusType { get; set; }
        public int CompanyId { get; set; }

        [JsonIgnore]
        [AllowNull]
        public Company Company { get; set; }
    }
}
