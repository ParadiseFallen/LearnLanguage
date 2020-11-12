using System.Globalization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using SharedModels.JsonConverters;
using Models.Models;

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

        [Required]
        [StringLength(500)]
        [JsonPropertyName("text")]
        public string Text { get; set; }

        [Required]
        [JsonPropertyName("culture")]
        public Language Culture { get; set; }

        /*Add TTS*/
        [NotMapped]
        [JsonPropertyName("sound")]
        public object Sound { get; set; }
    }
}
