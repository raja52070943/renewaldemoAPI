using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClarityImplementation.API.Models
{
    [Table("api_notification_logs")]
    public class Api_Notification_Log
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("notification_id")]
        public int NotificationId { get; set; }
        [Column("user_id")]
        public string? UserId { get; set; }
        [Column("destination_address")]
        public string? DestinationAddress { get; set; }
        [Column("notification_source")]
        public string? NotificationSource { get; set; }
        [Column("notification_type")]
        public string? NotificationType { get; set; }
        [Column("notification_category")]
        public string? NotificationCategory { get; set; }
        [Column("notification_sub_category")]
        public string? NotificationSubCategory { get; set; }
        [Column("notification_content")]
        public string? NotificationContent { get; set; }
        [Column("notification_sent_at")]
        public DateTime? NotificationSentAt { get; set; }
        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }
        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }
        [Column("created_by")]
        public string? CreatedBy { get; set; }
        [Column("updated_by")]
        public string? UpdatedBy { get; set;}
        [Column("notification_status")]
        public string? NotificationStatus { get; set; }
        [Column("case_id")]
        public string? CaseId { get; set; }
        [Column("case_owner_id")]
        public string? CaseOwnerId { get; set; }
        [Column("notification_label")]
        public string? NotificationLabel { get; set; }
        [Column("sf_task_id")]
        public string? SfTaskId { get; set; }
        [Column("mailbox_uid")]
        public string? MailboxUId { get; set; }
        [Column("mailbox_address")]
        public string? MailboxAddress { get; set; }
        [Column("mailbox_folder_name")]
        public string? MailboxFolderName { get; set;}

    }
}
