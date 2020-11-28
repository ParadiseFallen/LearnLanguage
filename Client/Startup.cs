using Avalonia;
using Client.ViewModels;
using Client.Views;
using Models.Services.API;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Linq;
using Client.Services;
using System.Security.Cryptography;
using Models.Services;
using Client.Models;
using CommandLine;

namespace Client
{
    public static class Startup
    {
        /// <summary>
        /// Inject classes
        /// </summary>
        /// <param name="builder">Support chain</param>
        /// <returns></returns>
        public static AppBuilder SetupAppConfiguration(this AppBuilder builder, string[] args)
        {
            var serializerOption = new JsonSerializerOptions();
            JsonExtension.GetAllConverters().ToList().ForEach(serializerOption.Converters.Add);

            var cfgService = new LocalSettingsService()
            {
                Options = serializerOption,
                OnLoad = data =>
                {
                    //decrypt
                },
                OnSave = data =>
                {
                    //encrtypt
                }
            };
            cfgService.Load();
            //test!
            var startupArgs = Parser.Default.ParseArguments<StartupArgs>(args).WithParsed(
                opt =>
                {
                    cfgService.Config.RestAPIAddres = string.IsNullOrEmpty(opt.ServerAddres) ? cfgService.Config.RestAPIAddres : opt.ServerAddres;
                    Console.WriteLine(cfgService.Config.RestAPIAddres);
                    cfgService.Save();
                });

            Console.WriteLine(cfgService.Config.RestAPIAddres);
            var http = new HttpApiClient(new Uri(cfgService.Config.RestAPIAddres));

            Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetExecutingAssembly());
            Locator.CurrentMutable.RegisterConstant(http, typeof(HttpApiClient));
            Locator.CurrentMutable.Register(() => new AccountAPIService(http, serializerOption), typeof(AccountAPIService));
            Locator.CurrentMutable.Register(() => new TranslateAPIService(http, serializerOption), typeof(TranslateAPIService));
            Locator.CurrentMutable.Register(() => new LanguageAPIService(http, serializerOption), typeof(LanguageAPIService));

            Locator.CurrentMutable.RegisterConstant(cfgService, typeof(ILocalSettingsService));

            cfgService.Save();
            return builder;
        }
    }
}
