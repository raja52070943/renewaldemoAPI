using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Drawing.Drawing2D;
using System.Numerics;
using System.Text.Json.Serialization;

namespace ClarityImplementation.API.Models.Plans
{
    public class EmployeeBenefitsPlan
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [DefaultValue("false")]
        public string IsMidYearPlan { get; set; }
        [DefaultValue("false")]
        public string IsPriorYearPlan { get; set; }
        public List<MidYearPlan> MidYearPlans { get; set; }
        public List<PriorYearPlan> PriorYearPlans { get; set; }
        public List<EmployeeBenefitsHRA> EmployeeBenefitsHRAs { get; set; }
        public List<PlanType> PlanTypes { get; set; }
        public List<PayScheduleType> PayScheduleTypes { get; set; }
        public EmployeeBenefitsGeneralInformation EmployeeBenefitsGeneralInformation { get; set; }
        [DefaultValue("false")]
        public string IsSamePlanEligibility { get; set; }
        public EmployeeBenefitsEnrollment EmployeeBenefitsEnrollment { get; set; }
        public EmployeeBenefitsFSA EmployeeBenefitsFSA { get; set; }
        public EmployeeBenefitsHSA EmployeeBenefitsHSA { get; set; }
        public EmployeeBenefitsSmartRide EmployeeBenefitsSmartRide { get; set; }
        [Range(0, 100)]
        public int Progress { get; set; }
        public int CompanyId { get; set; }
        [JsonIgnore]
        [AllowNull]
        public Company Company { get; set; }
    }
}
