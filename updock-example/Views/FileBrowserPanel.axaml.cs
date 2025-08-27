using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace updock_example.Views;

public partial class FileBrowserPanel : UserControl
{
    public FileBrowserPanel()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}