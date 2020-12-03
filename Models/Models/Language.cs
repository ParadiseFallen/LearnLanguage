using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Globalization;
using System.Text.Json.Serialization;

namespace Models
{ 
    public class Language
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("flag")]
        public Image Flag { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("culture")]
        public CultureInfo CultureInfo { get; set; }
    }
}
