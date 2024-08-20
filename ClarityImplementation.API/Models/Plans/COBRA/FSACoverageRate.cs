using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace ClarityImplementation.API.Models.Plans.COBRA
{
    public class FSACoverageRate
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string CoverageLevelName { get; set; }
        public string CurrentRate { get; set; }
        public string PriorRate { get; set; }
        public string FutureRate { get; set; }

        public int FSAPlanId { get; set; }
        [JsonIgnore]
        [AllowNull]
        public COBRAFSAPlan COBRAFSAPlan { get; set; }
    }
}
