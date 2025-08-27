using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace updock_example.Views;

public partial class PropertyPanel : UserControl
{
    public PropertyPanel()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}