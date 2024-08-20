using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace ClarityImplementation.API.Models.Plans
{
    public class EmployeeBenefitsLPFSA
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [DefaultValue("true")]
        public string IsLPFSAChecked { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        [DefaultValue("true")]
        public string IsStandardPlan { get; set; }

        public string IsTestStandardPlan { get; set; }
        public string IsCustomizePlan { get; set; }
        public string MinAnnualElectionAmount { get; set; }
        public string MaxAnnualElectionAmount { get; set; }
        public string EmployerContribution { set; get; }
        public string ActiveEmployeeRunoutPeriod { get; set; }
        public string TerminatedEmployeeRunoutPeriod { get; set; }
        public string OtherOptionForActive { get; set; }
        public string OtherOptionForTerminated { get; set; }
        public string TerminatedEmployeeCoverageEndPeriod { get; set; }
        public string AllowCarryOver { get; set; }
        public string IsTestAllowCarryOver { get; set; }
        public string AllowGracePeriod { get; set; }
        public string IsTestAllowGracePeriod { get; set; }
        public int TotalDays { get; set; }
        public string RunOutPeriod { get; set; }
        public int EmployeeBenefitsFSAId { get; set; }
        [JsonIgnore]
        [AllowNull]
        public EmployeeBenefitsFSA EmployeeBenefitsFSA { get; set; }
    }
}
