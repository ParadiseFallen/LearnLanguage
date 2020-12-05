using ApiClient;
using ApiServices.Extensions;
using ApiServices.Services;
using ApiServices.ServicesInterfaces;
using Client.Services;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;

namespace Client.ViewModels
{
    public class MainWindowVM : ReactiveObject, IScreen
    {
        public RoutingState Router { get; } 
        private ILocalSettingsService ConfigService { get; }
        public MainWindowVM()
        {
            Router = new RoutingState();
            //this.Router.Navigate.Execute(new MainMenuVM(this));
            //return;
            ConfigService = Locator.Current.GetService<ILocalSettingsService>();
            Init();
        }

        private async void Init()
        {
            var isAvailable = await Locator.Current.GetService<ApiService>().IsApiAvailable();
            var token = ConfigService.Config.AuthToken;
            if (string.IsNullOrEmpty(token)|| !isAvailable)
                this.Router.Navigate.Execute(new AuthVM(this));
            else
            {
                Locator.Current.GetService<IAccountService>().AuthCookie = token;
                this.Router.Navigate.Execute(new MainMenuVM(this));
            }
        }

    }
}
