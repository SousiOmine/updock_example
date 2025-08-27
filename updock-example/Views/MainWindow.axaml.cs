using Avalonia.Controls;
using Avalonia.Platform.Storage;
using System;
using System.Linq;
using System.Threading.Tasks;
using updock_example.ViewModels;

namespace updock_example.Views;

public partial class MainWindow : Window
{
    private IDisposable? _openFolderCommandSubscription;

    public MainWindow()
    {
        InitializeComponent();
        DataContextChanged += MainWindow_DataContextChanged;
    }

    private void MainWindow_DataContextChanged(object? sender, EventArgs e)
    {
        // 以前の購読を解除
        _openFolderCommandSubscription?.Dispose();
        _openFolderCommandSubscription = null;

        if (DataContext is MainWindowViewModel viewModel)
        {
            // OpenFolderCommand の実行を処理
            _openFolderCommandSubscription = viewModel.OpenFolderCommand.Subscribe(async _ => await OnOpenFolderAsync());
        }
    }

    private async Task OnOpenFolderAsync()
    {
        var storageProvider = StorageProvider;
        var folders = await storageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions
        {
            Title = "フォルダを選択してください",
            AllowMultiple = false
        });

        if (folders.Count > 0)
        {
            var selectedFolder = folders[0];
            if (DataContext is MainWindowViewModel viewModel)
            {
                viewModel.FileBrowser.SetRootDirectory(selectedFolder.Path.LocalPath);
            }
        }
    }
}