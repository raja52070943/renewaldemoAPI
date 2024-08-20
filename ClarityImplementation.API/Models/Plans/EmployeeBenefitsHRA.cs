using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace ClarityImplementation.API.Models.Plans
{
    public class EmployeeBenefitsHRA
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public string HRAType { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string ActiveEmployeeRunoutPeriod { get; set; }
        public string TerminatedEmployeeRunoutPeriod { get; set; }
        public string ActiveDays { get; set; }
        public string TerminatedDays { get; set; }
        public string ReimbursableExpenses { get; set; }
        [NotMapped]
        public List<int> SelectedReimbursableExpenses { get; set; }
        public string OtherExpenses { get; set; }
        public string CarrierName { get; set; }
        public string PlanName { get; set; }
        public string Employee { get; set; }
        public string EmployeeChild { get; set; }
        public string EmployeeSpouse { get; set; }
        public string Family { get; set; }
        [DefaultValue("false")]

        public string IsProRated { get; set; }
        [DefaultValue("false")]

        public string IsEmployeeResponsible { get; set; }
        public string ResponsibleEmployee { get; set; }
        public string ResponsibleEmployeeChild { get; set; }
        public string ResponsibleEmployeeSpouse { get; set; }
        public string ResponsibleFamily { get; set; }
        [DefaultValue("false")]

        public string IsHRAEmployeeResponsible { get; set; }
        public string HRAResponsibleEmployee { get; set; }
        public string HRAResponsibleEmployeeChild { get; set; }
        public string HRAResponsibleEmployeeSpouse { get; set; }
        public string HRAResponsibleFamily { get; set; }
        public string MaximumReimbursement { get; set; }
        [DefaultValue("false")]

        public string IsParticipantResponsible { get; set; }
        public string HRAClaimPercentage { get; set; }
        [DefaultValue("newplan")]
        public string HRAUnusedFund { get; set; }
        public string MaxRolloverAmount { get; set; }
        public string UnusedFundsPercentage { get; set; }
        [DefaultValue("fsa")]
        public string IsHRAorFSA { get; set; }
        [DefaultValue("false")]

        public string IsPlanForClarityBenefitCard { get; set; }
        public string ClarityBenefitCardType { get; set; }

        public string IsDependentCard { get; set; }
        public string IsDependentCardOption { get; set; }
        public string AdditionalPlanDetails { get; set; }
        public int EmployeeBenefitsPlanId { get; set; }
        [JsonIgnore]
        [AllowNull]
        public EmployeeBenefitsPlan EmployeeBenefitsPlan { get; set; }


    }
}
