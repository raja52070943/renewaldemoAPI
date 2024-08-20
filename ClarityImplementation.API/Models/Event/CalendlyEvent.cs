using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace ClarityImplementation.API.Models.Event
{
    public class CalendlyEvent
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string assigned_to { get; set; }
        public Guid event_type_uuid { get; set; }
        public string event_type_name { get; set; }
        public string event_start_time { get; set; }
        public string event_end_time { get; set; }
        public Guid invitee_uuid { get; set; }
        public string invitee_full_name { get; set; }
        public string invitee_email { get; set; }
        public string answer_1 { get; set; }
        public string answer_2 { get; set; }

        public int CompanyId { get; set; }
        [JsonIgnore]
        [AllowNull]
        public Company Company { get; set; }
    }
}
