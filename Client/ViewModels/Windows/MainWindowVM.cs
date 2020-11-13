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
            ConfigService = Locator.Current.GetService<ILocalSettingsService>();
            var canAccesApiTask = Locator.Current.GetService<HttpApiClient>().IsActive();
            canAccesApiTask.Wait();
            var token = ConfigService.Config.AuthToken;
            if (string.IsNullOrEmpty(token)|| !canAccesApiTask.Result)
                this.Router.Navigate.Execute(new AuthVM(this));
            else
            {
                Locator.Current.GetService<HttpApiClient>().AuthToken = token;
                this.Router.Navigate.Execute(new MainMenuVM(this));
            }
        }

    }
}
