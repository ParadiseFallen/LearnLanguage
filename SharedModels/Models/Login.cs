using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text;
using System.Text.Json.Serialization;

namespace SharedModels.Models
{
    public class Login
    {
        [Required]
        [JsonPropertyName("username")]
        [Index(IsUnique =true)]
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
