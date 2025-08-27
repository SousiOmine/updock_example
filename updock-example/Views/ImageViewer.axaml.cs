using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace updock_example.Views;

public partial class ImageViewer : UserControl
{
    public ImageViewer()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}