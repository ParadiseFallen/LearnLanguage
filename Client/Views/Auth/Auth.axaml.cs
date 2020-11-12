using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Client.ViewModels;
using ReactiveUI;

namespace Client.Views
{
    public class AuthView : ReactiveUserControl<AuthVM>
    {
        public AuthView()
        {
            AvaloniaXamlLoader.Load(this);
            this.WhenActivated(disposables => { });
        }
    }
}
