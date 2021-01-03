using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Client.ViewModels.Menu;

namespace Client.Views
{
    public class LanguagesEditor : ReactiveUserControl<LanguagesEditorVM>
    {
        public LanguagesEditor()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
