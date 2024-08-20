using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClarityImplementation.API.Models
{
    public class Email
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public string RecipientName { get; set; }
        public string Isname { get; set; }
        public string PositionTitle { get; set; }
        public string Isemail { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImplementationContactEmail { get; set; }
        public string CompanyName { get; set; }
        public int CompanyId { get; set; }

        public string CaseOwnerFirstName { get; set; }
        public string CaseOwnerLastName { get; set; }

        public string CaseOwnerEmail { get; set; }


        public string Template { get; set; }
        public int NoOfReminders { get; set; }

        public int NoOfDays { get; set; }

        public DateTime EmailSentdate { get; set; }

        public string CaseID { get; set; }
    }
}
