using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClarityImplementation.API.Models.Kickoff
{
    public class KickoffUserData
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }

        public string Role { get; set; }

        public string CalendlyUrl { get; set; }
        public DateTime DateAdded { get; set; }
        public string IsCalenderConnected { get; set; }
        public DateTime PreviousEventDate { get; set; }
        public int DaysSincePreviousEvent { get; set; }
        public int TotalEventsInLast30Days { get; set; }
        public int LifeTimeEvents { get; set; }
        public string VideoConferrencingIntegration { get; set; }

        public string CalendlyAPIKey { get; set; }
    }
}
