﻿using Avalonia;
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

namespace Client
{
    public static class Extensions
    {
        /// <summary>
        /// Inject classes
        /// </summary>
        /// <param name="builder">Support chain</param>
        /// <returns></returns>
        public static AppBuilder RegisterInjections(this AppBuilder builder)
        {
            var http = new HttpApiClient(new Uri("https://localhost:44312"));
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

            Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetExecutingAssembly());
            Locator.CurrentMutable.RegisterConstant(http, typeof(HttpApiClient));
            Locator.CurrentMutable.Register(()=> new AccountAPIService(http, serializerOption), typeof(AccountAPIService));
            Locator.CurrentMutable.Register(() => new TranslateAPIService(http, serializerOption), typeof(TranslateAPIService));
            Locator.CurrentMutable.RegisterConstant(cfgService, typeof(ILocalSettingsService));

            return builder;
        }
    }
}
