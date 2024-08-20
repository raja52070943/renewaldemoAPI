using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace ClarityImplementation.API.Models.Plans
{
    public class EmployeeBenefitsSmartRide
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [DefaultValue("true")]
        public string IsParkingChecked { get; set; }
        [DefaultValue("true")]
        public string IsTransitChecked { get; set; }
        public string ParkingStartDate { get; set; }
        public string TransitStartDate { get; set; }
        public string ParkingPreTax { get; set; }
        public string TransitPreTax { get; set; }
        [DefaultValue("false")]
        public string IsParkingPostTax { get; set; }
        [DefaultValue("false")]
        public string IsTransitPostTax { get; set; }
        public string ParkingOption { get; set; }
        public string TransitOption { get; set; }
        public string ParkingRunoutPeriod { get; set; }
        public string OtherOptionForParking { get; set; }
        public string OtherOptionForTransit { get; set; }
        public string TransitRunoutPeriod { get; set; }
        [DefaultValue("true")]
        public string IsParkingAddPlan { get; set; }
        [DefaultValue("true")]
        public string IsTransitAddPlan { get; set; }
        public int EmployeeBenefitsPlanId { get; set; }
        [JsonIgnore]
        [AllowNull]
        public EmployeeBenefitsPlan EmployeeBenefitsPlan { get; set; }
    }
}
