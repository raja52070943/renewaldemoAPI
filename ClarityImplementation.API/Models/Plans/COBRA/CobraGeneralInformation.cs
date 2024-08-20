using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace ClarityImplementation.API.Models.Plans.COBRA
{
    public class CobraGeneralInformation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string IsChange { get; set; } // Indicates if the settings are changing(e.g., "Yes" or "No")
        public string ClarityEventReceiptionMethod { get; set; }  // Dropdown for how Clarity will receive new events and participants
        public string VendorName { get; set; }
        public string VendorContactFirstName { get; set; }
        public string VendorContactLastName { get; set; }
        public string VendorContactEmail { get; set; }
        public string CurrentBenefitAdministrationPlatform { get; set; } // Current benefit administration platform selection
        public string OtherBenefitAdminPlatform { get; set; } // If "Other" is selected for benefit administration platform
        public string CurrentPayrollPlatform { get; set; } // Current payroll platform selection
        public string OtherPayrollPlatform { get; set; } // If "Other" is selected for payroll platform
        public int COBRAPlanId { get; set; }
        [JsonIgnore]
        [AllowNull]
        public COBRAPlan COBRAPlan { get; set; }
    }
}
