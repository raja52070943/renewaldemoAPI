using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics.CodeAnalysis;

namespace ClarityImplementation.API.Models
{
    [Table("api_case_contacts")]
    public class Api_Case_Contact
    {
        [Key]
        [Column("case_contact_id")]
        public string CaseContactId { get; set; }


        [Column("case_id")]
        public string CaseId { get; set; }

        [Column("sf_contact_id")]
        public string? SfContactId { get; set; }



        [DefaultValue("New")]
        [Column("status")]
        public string? Status { get; set; }


        [Column("version_no")]
        public int? VersionNo { get; set; }



        [DefaultValue("CLIENT")]
        [Column("contact_type")]
        public string? ContactType { get; set; }

        [Column("organization_name")]
        public string? OrganizationName { get; set; }

        [Column("organization_state")]
        public string? OrganizationState { get; set; }

        [Column("organization_zip")]
        public string? OrganizationZip { get; set; }

        [Column("contact_sub_type")]
        public string? ContactSubType { get; set; }


        [DefaultValue("HR")]
        [Column("contact_title")]
        public string? ContactTitle { get; set; }

        [Column("contact_first_name")]
        public string? ContactFirstName { get; set; }

        [Column("contact_last_name")]
        public string? ContactLastName { get; set; }

        [Column("contact_email")]
        public string? ContactEmail { get; set; }

        [Column("contact_phone")]
        public string? ContactPhone { get; set; }

        [Column("contact_is_cons_ben_contact")]
        public int? ContactIsConsBenContact { get; set; }

        [Column("contact_is_cobra_contact")]
        public int? ContactIsCobraContact { get; set; }

        [Column("contact_is_implementation_contact")]
        public int? ContactIsImplementationContact { get; set; }


        [Column("contact_is_ben_admin_contact")]
        public int? ContactIsBenAdminContact { get; set; }


        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("created_by")]
        public string? CreatedBy { get; set; }


        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [Column("updated_by")]
        public string? UpdatedBy { get; set; }

        [Column("contact_email_org")]
        public string? ContactEmailOrg { get; set; }
        [Column("benefit_plan_type")]

        public string? BenefitPlanType { get; set; }

        [Column("contact_is_participant_contact")]
        public int? ContactIsParticipantContact { get; set; }



        [DefaultValue("0")]
        [Column("is_invited")]
        public string? IsInvited { get; set; }

        [DefaultValue("No")]
        [Column("is_case_collaborator")]
        public string? IsCaseCollaborator { get; set; }

        [DefaultValue("No")]
        [Column("is_primary_contact")]
        public string? IsPrimaryContact { get; set; }

        public string Role { get; set; }
        //[NotMapped]
        //public List<string> SelectedRoles { get; set; }
        public string Responsibility { get; set; }
        //[NotMapped]
        //public List<string> SelectedResponsibilities { get; set; }
        public int? CompanyId { get; set; }
        [JsonIgnore]
        [AllowNull]
        public Company Company { get; set; }

    }
}
