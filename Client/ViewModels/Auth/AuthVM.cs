using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Client.ViewModels
{
    public class AuthVM : ReactiveObject, IRoutableViewModel, IScreen
    {
        #region IRoutableViewModel
        public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);
        public IScreen HostScreen { get; private set; }
        public RoutingState Router { get; }
        #endregion
        public AuthVM()
        {
        }
        public AuthVM(IScreen hostScreen = null)
        {
            Router = new RoutingState();
            HostScreen = hostScreen;
            Router.Navigate.Execute(new LoginVM(this));
        }
    }
}
