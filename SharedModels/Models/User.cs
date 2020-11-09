using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.Json.Serialization;

namespace SharedModels.Models
{
    public class User : IdentityUser
    {
        [JsonPropertyName("nativeLanguage")]
        [Required]
        public CultureInfo NativeLanguage { get; set; }
    }
}
