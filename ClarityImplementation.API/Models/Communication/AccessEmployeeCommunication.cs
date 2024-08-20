using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClarityImplementation.API.Models.Communication
{
    public class AccessEmployeeCommunication
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string PlanName { get; set; }
        public string FileCategory { get; set; }
        public string FileType { get; set; }
        public string FileUrl { get; set; }
    }
}
