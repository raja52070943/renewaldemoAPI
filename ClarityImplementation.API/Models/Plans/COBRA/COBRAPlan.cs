using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace ClarityImplementation.API.Models.Plans.COBRA
{
    public class COBRAPlan
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public CobraGeneralInformation CobraGeneralInformation { get; set; }
        public List<COBRAMedicalPlan> COBRAMedicalPlans { get; set; }
        public List<COBRADentalPlan> COBRADentalPlans { get; set; }
        public List<COBRAVisionPlan> COBRAVisionPlans { get; set; }
        public List<COBRAHRAPlan> COBRAHRAPlans { get; set; }
        public List<COBRAFSAPlan> COBRAFSAPlans { get; set; }
        public List<COBRAEAPPlan> COBRAEAPPlans { get; set; }
        public List<COBRAInsurancePlan> COBRAInsurancePlans { get; set; }
        public List<EnrollmentAndEligibilityContact> EnrollmentAndEligibilityContact { get; set; }
        public CobraOpenEnrollmentManagement CobraOpenEnrollmentManagement { get; set; }
        public string IsOpenEnrollment { set; get; }
        [Range(0, 100)]
        public int Progress { get; set; }
        public int CompanyId { get; set; }
        [JsonIgnore]
        [AllowNull]
        public Company Company { get; set; }
    }
}
