using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;

namespace Client.ViewModels
{
    public abstract class RoutableViewModel : ReactiveObject, IRoutableViewModel
    {
        #region IRoutableViewModel
        public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);
        public IScreen HostScreen { get; protected set; }
        #endregion
        public RoutableViewModel(IScreen hostScreen)
        {
            HostScreen = hostScreen;
        }
    }
}
