using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace SharedModels.Models
{
    public class Login
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [JsonPropertyName("username")]
        [Index(IsUnique =true)]
        public string Username { get; set; }
        [Required]
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
