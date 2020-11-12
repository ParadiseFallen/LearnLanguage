using Microsoft.AspNetCore.Identity;
using Models.Models;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.Json.Serialization;

namespace SharedModels.Models
{
    public class User : IdentityUser
    {
        [JsonPropertyName("nativeCulture")]
        [Required]
        public Language NativeCulture { get; set; }
    }
}
