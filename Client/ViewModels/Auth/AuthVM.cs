using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Client.ViewModels
{
    public class AuthVM : RoutableViewModel, IScreen
    {
        #region IRoutableViewModel
        public RoutingState Router { get; }
        #endregion
        //require for avalonia debug
        public AuthVM() : base(null)
        {
        }
        public AuthVM(IScreen hostScreen = null) : base(hostScreen)
        {
            Router = new RoutingState();
            Router.Navigate.Execute(new LoginVM(this));
        }
    }
}
