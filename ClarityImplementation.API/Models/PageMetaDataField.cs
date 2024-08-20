using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace ClarityImplementation.API.Models
{
    public class PageMetaDataField
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string FieldMetaId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Tooltip { get; set; }
        public string Placeholder { get; set; }
        public string Regex { get; set; }
        public string ErrorMessage { get; set; }
        public int PageId { get; set; }
        [JsonIgnore]
        [AllowNull]
        public Page Page { get; set; }
    }
}
