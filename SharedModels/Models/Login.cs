using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.Json.Serialization;

namespace Models
{
    [Index("Username",IsUnique =true)]
    public class Login
    {
        [Required]
        [JsonPropertyName("username")]
        public string Username { get; set; }
        [Required]
        [JsonPropertyName("password")]
        public string Password { get; set; }
        [Required]
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [Required]
        [JsonPropertyName("culture")]
        public CultureInfo Culture { get; set; }
    }
}
