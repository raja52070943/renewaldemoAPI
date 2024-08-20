using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace ClarityImplementation.API.Models
{
    public class CompanyContact
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        [Column("case_id")]
        public string CaseId { get; set; }
        [Column("sf_contact_id")]
        public string SFContactId { get; set; }
        [Column("case_contact_id")]
        public string CaseContactId { get; set; }

        public string Role { get; set; }
        [NotMapped]
        public List<string> SelectedRoles { get; set; }
        public string Responsibility { get; set; }
        [NotMapped]
        public List<string> SelectedResponsibilities { get; set; }

        public string? IsInvite { get; set; }
        public int? ContactIsImplementationContact { get; set; }

        public string IsPrimaryContact { get; set; }
        public int CompanyId { get; set; }
        [JsonIgnore]
        [AllowNull]
        public Company Company { get; set; }
    }
}
