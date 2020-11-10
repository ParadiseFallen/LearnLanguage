using Client.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Client.Services
{
    public class PreferenceService
    {
        public PreferenceService()
        {

        }
        public LocalPrefs GetPrefs()
        {
            return default;
        }

        static private LocalPrefs prefs;
        //static public LocalPrefs Prefs 
        //{ 
        //    get 
        //    {
        //        if (prefs == null)
        //        {

        //        }
        //    }
        //    set 
        //    { 
            
        //    } 
        //}


        static public void SaveLocalStore(LocalPrefs prefs)
        {
            //ConfigurationSettings.AppSettings.
            Configuration config =
                        ConfigurationManager.OpenExeConfiguration(
                        ConfigurationUserLevel.None);
            prefs.SectionInformation.ForceSave = true;

            //Console.WriteLine(prefs.SectionInformation.);
            config.Save(ConfigurationSaveMode.Full);
            Console.WriteLine(config.FilePath);
            config.SaveAs("text.json");
            //RsaProtectedConfigurationProvider
            Console.ReadKey();
        }
        static public LocalPrefs GetOrCreate()
        {
            Configuration config =
                           ConfigurationManager.OpenExeConfiguration(
                           ConfigurationUserLevel.None);
            return (LocalPrefs)config.Sections[nameof(LocalPrefs)] == null?new LocalPrefs(): (LocalPrefs)config.Sections[nameof(LocalPrefs)];
        }
    }
}
