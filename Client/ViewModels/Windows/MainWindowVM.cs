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
            ConfigService = Locator.Current.GetService<ILocalSettingsService>();
            Router = new RoutingState();
            var token = ConfigService.Config.AuthToken;
            if (string.IsNullOrEmpty(token)||true)
                this.Router.Navigate.Execute(new AuthVM(this));
            else
            {
                Locator.Current.GetService<HttpApiClient>().AuthToken = token;
                this.Router.Navigate.Execute(new MainMenuVM(this));
            }
        }

    }
}
