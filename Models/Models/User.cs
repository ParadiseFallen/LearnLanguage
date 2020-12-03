using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class User : IdentityUser
    {
        [JsonPropertyName("nativeCulture")]
        [Required]
        public Language NativeCulture { get; set; }
    }
}
