using Client.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace Client.Services
{
    public class LocalSettingsService : ILocalSettingsService
    {
        public string Filename { get; }
        public string FilePath { get; }
        private FileInfo Info { get; }
        public JsonSerializerOptions Options { get; set; }
        public Action<string> OnLoad { get; set; }
        public Action<string> OnSave { get; set; }

        private LocalSettings config;
        public LocalSettings Config
        {
            get
            {
                if (config == null)
                    this.Load();
                return config;
            }
        }

        public LocalSettingsService(string filePath = ".", string filename = "app.cfg")
        {
            Filename = filename;
            FilePath = filePath;
            Info = new FileInfo($"{FilePath}/{Filename}");
        }
        public ILocalSettingsService Load()
        {
            try
            {
                if (Info.Exists)
                    using (StreamReader streamReader = new StreamReader($"{FilePath}/{Filename}"))
                    {
                        var data = streamReader.ReadToEnd();
                        OnLoad?.Invoke(data);
                        config = JsonSerializer.Deserialize<LocalSettings>(data, Options);
                    }
                else
                    config = LocalSettings.Default;
            }
            catch (Exception ex)
            {
                Console.WriteLine("configuration not found. use default configuration."); //replace to logger
                config = LocalSettings.Default;
            }
            finally
            { 
                Save();
            }
            return this;
        }

        public ILocalSettingsService Save()
        {
            try
            {
                if (!Info.Exists)
                    Info.Create().Close();
                using (StreamWriter sw = new StreamWriter($"{FilePath}/{Filename}", false))
                    sw.Write(JsonSerializer.Serialize(config, Options));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return this;
        }
        
    }
}
