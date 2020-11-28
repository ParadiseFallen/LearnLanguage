using Client.Services;
using Models.Services.API;
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
            //this.Router.Navigate.Execute(new MainMenuVM(this));
            //return;
            var client = Locator.Current.GetService<HttpApiClient>();
            var canAccesApiTask = await client.IsActive();
            var token = ConfigService.Config.AuthToken;
            if (string.IsNullOrEmpty(token)|| !canAccesApiTask)
                this.Router.Navigate.Execute(new AuthVM(this));
            else
            {
                Locator.Current.GetService<HttpApiClient>().AuthToken = token;
                this.Router.Navigate.Execute(new MainMenuVM(this));
            }
        }

    }
}
