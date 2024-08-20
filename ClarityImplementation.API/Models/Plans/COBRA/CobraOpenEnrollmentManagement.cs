using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace ClarityImplementation.API.Models.Plans.COBRA
{
    public class CobraOpenEnrollmentManagement
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string OpenEnrollmentMethod { get; set; }
        public int NoOfDaysOffered { get; set; }
        public int COBRAPlanId { get; set; }
        [JsonIgnore]
        [AllowNull]
        public COBRAPlan COBRAPlan { get; set; }
    }
}
