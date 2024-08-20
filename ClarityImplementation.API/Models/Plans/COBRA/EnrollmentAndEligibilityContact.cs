using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace ClarityImplementation.API.Models.Plans.COBRA
{
    public class EnrollmentAndEligibilityContact
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string ContactType { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactDescription { get; set; }
        public string CarrierType { get; set; }
        public string CarrierName { get; set; }
        public string BenefitType { get; set; }
        public string BenefitName { get; set; }
        public string ContactStatus { get; set; }
        public int COBRAPlanId { get; set; }
        [JsonIgnore]
        [AllowNull]
        public COBRAPlan COBRAPlan { get; set; }

    }
}
