using Avalonia;
using ReactiveUI;
using Splat;
using System;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Linq;
using Client.Services;
using Client.Models;
using CommandLine;
using ApiClient;
using ApiServices.ServicesInterfaces;
using ApiServices.Services;
using System.Net.Http;

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
            serializerOption.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

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
            var http = new RestClient(new HttpClientHandler()) {BaseAddress=new Uri(cfgService.Config.RestAPIAddres) };

            #region register services

            Locator.CurrentMutable.RegisterConstant(http, typeof(RestClient));

            Locator.CurrentMutable.Register(
                () => new AccountService(http) { SerializerOptions = serializerOption }, 
                typeof(IAccountService));

            Locator.CurrentMutable.Register(
                () => new TranslationService(http) { SerializerOptions = serializerOption }, 
                typeof(ITranslationService));

            Locator.CurrentMutable.Register(
                () => new LanguageService(http) { SerializerOptions = serializerOption }, 
                typeof(ILanguageService));

            Locator.CurrentMutable.Register(
                () => new ApiService(http) { SerializerOptions = serializerOption },
                typeof(ApiService));
            
            Locator.CurrentMutable.RegisterConstant(cfgService, typeof(ILocalSettingsService));

            #endregion

            Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetExecutingAssembly());

            cfgService.Save();
            return builder;
        }
    }
}
