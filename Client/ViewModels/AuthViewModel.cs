using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Client.ViewModels
{
    public class AuthViewModel : ReactiveObject, IRoutableViewModel, IScreen
    {
        #region IRoutableViewModel
        public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);
        public IScreen HostScreen { get; private set; }
        public RoutingState Router { get; }
        #endregion
        public AuthViewModel()
        {
        }
        public AuthViewModel(IScreen hostScreen = null)
        {
            Router = new RoutingState();
            HostScreen = hostScreen;
            Router.Navigate.Execute(new LoginViewModel(this));
        }
    }
}
