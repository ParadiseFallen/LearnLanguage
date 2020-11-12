using Client.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Client.Services
{
    public interface ILocalSettingsService
    {
        LocalSettings Config { get; }
        JsonSerializerOptions Options { get; } 
        Action<string> OnLoad { get; set; }
        Action<string> OnSave { get; set; }
        public ILocalSettingsService Save();
        public ILocalSettingsService Load();
    }
}
