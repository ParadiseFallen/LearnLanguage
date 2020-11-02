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

            Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetExecutingAssembly());

            Locator.CurrentMutable.RegisterConstant(http, typeof(HttpApiClient));
            Locator.CurrentMutable.Register(()=> new AccountAPIService(http), typeof(AccountAPIService));

            return builder;
        }
    }
}
