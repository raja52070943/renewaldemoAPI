using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace ClarityImplementation.API.Models.Plans.COBRA
{
    public class COBRAClientContact
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int EnrollmentAndEligibilityContactId { get; set; }
        [JsonIgnore]
        [AllowNull]
        public EnrollmentAndEligibilityContact EnrollmentAndEligibilityContact { get; set; }
    }
}
