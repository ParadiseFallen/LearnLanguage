using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Client.ViewModels;
using ReactiveUI;

namespace Client.Views
{
    public class LoginView : ReactiveUserControl<LoginVM>
    {
        public LoginView()
        {
            AvaloniaXamlLoader.Load(this);
            this.WhenActivated(disposables => { });
        }
    }
}
