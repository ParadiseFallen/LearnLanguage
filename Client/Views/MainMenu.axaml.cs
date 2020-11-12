using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;

namespace Client.Views
{
    public class MainMenuView : ReactiveUserControl<MainMenuView>
    {
        public MainMenuView()
        {
            AvaloniaXamlLoader.Load(this);
        }

    }
}
