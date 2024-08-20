using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClarityImplementation.API.Models
{
    [Table("api_file_uploads")]
    public class Api_File_Upload
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("file_upload_id")]
        public int FileUploadId { get; set; }
        [Column("case_id")]
        public string? CaseId { get; set; }
        [Column("uploaded_by_user_id")]
        public string? UploadedByUserId { get; set; }
        [Column("platform_name")]
        public string? PlatformName { get; set; }
        [Column("platform_template_name")]
        public string? PlatformTempleteName { get; set; }
        [Column("file_status")]
        public string? FileStatus { get; set; }
        [Column("file_name")]
        public string? FileName { get; set; }
        [Column("checker_results")]
        public string? CheckerResults { get; set; }
        [Column("upload_results")]
        public string? UploadResults { get; set; }
        [Column("upload_errors")]
        public string? UploadErrors { get; set; }
        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }
        [Column("created_by")]
        public string? CreatedBy { get; set; }
        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }
        [Column("updated_by")]
        public string? UpdatedBy { get; set; }
        [Column("sf_new_task_id")]
        public string? SfNewTaskId { get; set; }
        [Column("employer_name")]
        public string? EmployerName { get; set; }
        [Column("sf_case_owner_user_id")]
        public string? SfCaseOwnerUserId { get; set; }
        [Column("user_uploaded_file_name")]
        public string? UserUploadedFileName { get; set; }
        [Column("case_sub_type")]
        public string? CaseSubtype { get; set; }
        [Column("case_type")]
        public string? CaseType { get; set; }
    }
}
