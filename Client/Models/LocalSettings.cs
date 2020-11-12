using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Text.Json.Serialization;

namespace Client.Models
{
    public class LocalSettings 
    {
        public static LocalSettings Default 
        {
            get => new LocalSettings() 
            {
                AppCulture = new CultureInfo("en-EN"),
            };
        }
        public CultureInfo AppCulture { get; set; }

        public string Username { get; set; }
        public string AuthToken { get; set; }
    }
}
