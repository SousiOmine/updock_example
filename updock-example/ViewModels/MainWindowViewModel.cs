using System;
using System.IO;
using System.Reactive;
using ReactiveUI;
using updock_example.Models;

namespace updock_example.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    /// <summary>
    /// ファイルブラウザViewModel
    /// </summary>
    public FileBrowserViewModel FileBrowser { get; } = new();

    /// <summary>
    /// 画像プロパティViewModel
    /// </summary>
    public ImagePropertiesViewModel ImageProperties { get; } = new();

    /// <summary>
    /// サムネイルViewModel
    /// </summary>
    public ThumbnailViewModel Thumbnail { get; } = new();

    /// <summary>
    /// 画像ビューアViewModel
    /// </summary>
    public ImageViewerViewModel ImageViewer { get; } = new();

    /// <summary>
    /// フォルダを開くコマンド
    /// </summary>
    public ReactiveCommand<Unit, Unit> OpenFolderCommand { get; }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public MainWindowViewModel()
    {
        // ファイルブラウザのファイル選択イベントを登録
        FileBrowser.FileSelected += OnFileSelected;
        
        // サムネイルの選択イベントを登録
        Thumbnail.ThumbnailSelected += OnThumbnailSelected;
        
        // デフォルトのルートディレクトリを設定（デスクトップ）
        var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        FileBrowser.SetRootDirectory(desktopPath);

        // フォルダを開くコマンドを初期化
        OpenFolderCommand = ReactiveCommand.Create(() => { });
    }

    /// <summary>
    /// ファイルが選択されたときの処理
    /// </summary>
    /// <param name="sender">送信元</param>
    /// <param name="e">イベント引数</param>
    private void OnFileSelected(object? sender, FileSelectedEventArgs e)
    {
        var imageFile = new ImageFileModel(e.FilePath);
        UpdateImageView(imageFile);
    }

    /// <summary>
    /// サムネイルが選択されたときの処理
    /// </summary>
    /// <param name="sender">送信元</param>
    /// <param name="e">イベント引数</param>
    private void OnThumbnailSelected(object? sender, ThumbnailSelectedEventArgs e)
    {
        UpdateImageView(e.ImageFile);
    }

    /// <summary>
    /// 画像表示を更新
    /// </summary>
    /// <param name="imageFile">画像ファイル</param>
    private void UpdateImageView(ImageFileModel imageFile)
    {
        // 画像プロパティを更新
        ImageProperties.UpdateImageInfo(imageFile);
        
        // メイン画像を更新
        ImageViewer.UpdateImage(imageFile);
        
        // サムネイルのフォルダを更新（同じフォルダにある他の画像も表示）
        var folderPath = Path.GetDirectoryName(imageFile.FilePath);
        if (folderPath != null)
        {
            Thumbnail.UpdateThumbnails(folderPath);
        }
    }
}
