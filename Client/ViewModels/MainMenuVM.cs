using ReactiveUI;
using System;

namespace Client.ViewModels
{
    public class MainMenuVM : ReactiveObject, IRoutableViewModel
    {
        #region IRoutableViewModel
        public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);
        public IScreen HostScreen { get; private set; }
        #endregion

        #region Props
        public IReactiveCommand Learning { get; set; }
        public IReactiveCommand Check { get; set; }
        public IReactiveCommand Account { get; set; }
        public IReactiveCommand Exit { get; set; }
        #endregion

        #region Ctors
        public MainMenuVM(IScreen hostScreen)
        {
            HostScreen = hostScreen;
        }
        #endregion
    }
}
