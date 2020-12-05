using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models
{
    public record UserInfo
    {
        public string Username { get; init; }
        public Language NativeCulture { get; init; }
        public string Email { get; init; }
        public UserInfo() { }
        public UserInfo(string username, string email, Language nativeCulture) =>
            (Username, NativeCulture, Email) = (username, nativeCulture, email);
    }
}
