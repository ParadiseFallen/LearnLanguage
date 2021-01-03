using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Client.ViewModels.Menu;

namespace Client.Views.Menu
{
    public class Translations : ReactiveUserControl<TranslationsVM>
    {
        public Translations()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
