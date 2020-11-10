using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Client.Models
{
    //[ConfigurationPermission(System.Security.Permissions.SecurityAction.]
    public sealed class LocalPrefs : ConfigurationElement
    {
        //[ConfigurationProperty("AuthToken")]
        //public string AuthToken { get; set; }
        //[ConfigurationProperty("username")]
        //public string Username { get; set; }

        [ConfigurationProperty("Username")]
        public string Username
        {
            get
            {
                return (string)this["username"];
            }
            set
            {
                this["username"] = value;
            }
        }
    }
}
