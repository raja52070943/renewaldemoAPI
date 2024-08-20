using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace ClarityImplementation.API.Models.Plans.COBRA
{
    public class COBRAVisionPlan
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string CarrierName { get; set; }
        public string EligibilityContactEmailAddress { get; set; }
        public string Eight34ContactEmailAddress { get; set; }
        public string PlanName { get; set; }
        public string IncorporationState { get; set; }
        public string GroupNumber { get; set; }
        public string SubGroupNumber { get; set; }
        public string PlanRenewalDate { get; set; }
        public string InsuranceType { get; set; }
        public string IsDivisionSpecific { get; set; }
        public string DivisionName { get; set; }
        [NotMapped]
        public List<string> SelectedDivisionNames { get; set; }
        public string IsDisabilityExtension { get; set; }
        public string PlanRule { get; set; }
        public string PlanRateType { get; set; }
        public string CurrentRateStartDate { get; set; }
        public string CurrentRateEndDate { get; set; }

        public string PriorRateStartDate { get; set; }
        public string PriorRateEndDate { get; set; }

        public string FutureRateStartDate { get; set; }
        public string FutureRateEndDate { get; set; }

        public List<VisionCoverageRate> VisionCoverageRates { get; set; }

        public int COBRAPlanId { get; set; }
        [JsonIgnore]
        [AllowNull]
        public COBRAPlan COBRAPlan { get; set; }
    }
}
