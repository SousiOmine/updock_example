using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using updock_example.Models;
using updock_example.ViewModels;

namespace updock_example.Views;

public partial class ThumbnailPanel : UserControl
{
    public ThumbnailPanel()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void OnThumbnailClick(object sender, PointerPressedEventArgs e)
    {
        if (sender is Border border && border.DataContext is ThumbnailItem thumbnailItem)
        {
            var viewModel = DataContext as ThumbnailViewModel;
            viewModel?.SelectThumbnailCommand.Execute(thumbnailItem);
        }
    }
}