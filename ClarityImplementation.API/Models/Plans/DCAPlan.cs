using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClarityImplementation.API.Models.Plans
{
    public class DCAPlan
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public bool IsRecommended { get; set; }
        public float MinAnnualElectionAmount { get; set; }

        public float MaxAnnualElectionAmount { get; set; }
        public float EmployerContribution { set; get; }
        public int ActiveEmployeeRunoutPeriod { get; set; }
        public int TerminatedEmployeeRunoutPeriod { get; set; }
        public int TerminatedEmployeeCoverageEndPeriod { get; set; }
        public bool AllowCarryOver { get; set; }
        public bool AllowGracePeriod { get; set; }
        public int TotalDays { get; set; }
    }
}
