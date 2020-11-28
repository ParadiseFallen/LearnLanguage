using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Client.ViewModels;

namespace Client.Views
{
    public class MainMenuView : ReactiveUserControl<MainMenuVM>
    {
        public MainMenuView()
        {
            AvaloniaXamlLoader.Load(this);
        }

    }
}
