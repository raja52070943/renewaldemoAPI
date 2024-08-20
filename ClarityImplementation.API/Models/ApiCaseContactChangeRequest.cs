using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClarityImplementation.API.Models
{
    [Table("api_case_contact_change_requests")]
    public class ApiCaseContactChangeRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("case_contact_change_id")]
        public int CaseContactChangeId { get; set; }

        [Required]
        [StringLength(50)]
        [Column("case_contact_id")]
        public string CaseContactId { get; set; }

        [Required]
        [StringLength(50)]
        [Column("case_id")]
        public string CaseId { get; set; }

        [StringLength(50)]
        [Column("sf_contact_id")]
        public string? SfContactId { get; set; }

       
        [StringLength(50)]
        [DefaultValue("New")]
        [Column("status")]
        public string? Status { get; set; }

        [StringLength(2000)]
        [Column("sf_case_owner_user_id")]
        public string? SfCaseOwnerUserId { get; set; }

        [StringLength(200)]
        [Column("sf_task_id")]
        public string? SfTaskId { get; set; }

        
        [DefaultValue(0)]
        [Column("version_no")]
        public int? VersionNo { get; set; }

        
        [StringLength(50)]
        [DefaultValue("CLIENT")]
        [Column("contact_type")]
        public string? ContactType { get; set; }

        [StringLength(100)]
        [Column("organization_name")]
        public string? OrganizationName { get; set; }

        [StringLength(50)]
        [Column("organization_state")]
        public string? OrganizationState { get; set; }

        [StringLength(10)]
        [Column("organization_zip")]
        public string? OrganizationZip { get; set; }

        [StringLength(50)]
        [Column("contact_sub_type")]
        public string? ContactSubType { get; set; }

        
        [StringLength(50)]
        [DefaultValue("HR")]
        [Column("contact_title")]
        public string? ContactTitle { get; set; }

        [StringLength(100)]
        [Column("contact_first_name")]
        public string? ContactFirstName { get; set; }

        [StringLength(100)]
        [Column("contact_last_name")]
        public string? ContactLastName { get; set; }

        [StringLength(100)]
        [Column("contact_email")]
        public string? ContactEmail { get; set; }

        [StringLength(100)]
        [Column("contact_phone")]
        public string? ContactPhone { get; set; }

        
        [DefaultValue(false)]
        [Column("contact_is_cons_ben_contact")]
        public bool ContactIsConsBenContact { get; set; }

        
        [DefaultValue(false)]
        [Column("contact_is_cobra_contact")]
        public bool ContactIsCobraContact { get; set; }

        
        [DefaultValue(false)]
        [Column("contact_is_ben_admin_contact")]
        public bool ContactIsBenAdminContact { get; set; }

        
        [Column("created_at", TypeName = "datetime")]
        [DefaultValue("CURRENT_TIMESTAMP")]

        public DateTime CreatedAt { get; set; }

        [StringLength(200)]
        [DefaultValue("CURRENT_USER")]
        [Column("created_by")]
        public string CreatedBy { get; set; }

        
        [Column("updated_at", TypeName = "datetime")]
        [DefaultValue("CURRENT_TIMESTAMP")]
        public DateTime UpdatedAt { get; set; }

        [StringLength(200)]
        [DefaultValue("CURRENT_USER")]
        [Column("updated_by")]
        public string? UpdatedBy { get; set; }

        [StringLength(150)]
        [Column("employer_name")]
        public string? EmployerName { get; set; }

        [StringLength(50)]
        [Column("case_type")]
        public string? CaseType { get; set; }

        [StringLength(50)]
        [Column("case_sub_type")]
        public string? CaseSubType { get; set; }

        [StringLength(200)]
        [Column("requested_by_email")]
        public string? RequestedByEmail { get; set; }
    }
}
