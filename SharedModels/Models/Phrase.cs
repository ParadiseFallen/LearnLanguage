using System.Globalization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using SharedModels.JsonConverters;

namespace SharedModels.Models
{
    /// <summary>
    /// Store word properties.
    /// </summary>
    public class Phrase
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [StringLength(500)]
        [JsonPropertyName("text")]
        [Required]
        public string Text { get; set; }

        [JsonPropertyName("culture")]
        [Column(TypeName ="char(5)")]
        [Required]
        public CultureInfo Culture { get; set; }

        [NotMapped]
        [JsonPropertyName("sound")]
        public object Sound { get; set; }
    }
}
