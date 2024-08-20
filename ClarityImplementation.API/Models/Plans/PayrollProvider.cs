using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClarityImplementation.API.Models.Plans
{
    public class PayrollProvider
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string PayrollProviderName { get; set; }
    }
}
