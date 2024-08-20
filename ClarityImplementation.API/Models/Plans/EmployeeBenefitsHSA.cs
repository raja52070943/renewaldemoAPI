using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace ClarityImplementation.API.Models.Plans
{
    public class EmployeeBenefitsHSA
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [DefaultValue("false")]
        public string IsOfferHSA { get; set; }
        public string EnrolledEmployee { get; set; }
        [DefaultValue("false")]
            
        public string IsBulkTransfer { get; set; }
        public string BankName { get; set; }
        public string StartDate { get; set; }
        [DefaultValue("false")]

        public string IsPayrollAdvance { get; set; }
        public string MaximumAmount { get; set; }
        public string RepaymentAmount { get; set; }
        [DefaultValue("false")]

        public string IsFullBalanceAdvance { get; set; }
        [DefaultValue("false")]

        public string IsEmployerContribution { get; set; }
        public List<EmployeeContributionGroup> EmployeeContributionGroups { get; set; }
        [DefaultValue("false")]

        public string IsCardIssued { get; set; }
        [DefaultValue("Spouse Only")]

        public string IsSpouseOrDependent { get; set; }
        public int EmployeeBenefitsPlanId { get; set; }
        [JsonIgnore]
        [AllowNull]
        public EmployeeBenefitsPlan EmployeeBenefitsPlan { get; set; }
    }
}
