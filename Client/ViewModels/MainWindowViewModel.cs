using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;

namespace Client.ViewModels
{
    public class MainWindowViewModel : ReactiveObject, IScreen
    {
        public RoutingState Router { get; } 
        public MainWindowViewModel()
        {
            Router = new RoutingState();
            this.Router.Navigate.Execute(new LoginViewModel(this));
        }
        
    }
}
