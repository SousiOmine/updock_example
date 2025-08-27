using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reactive;
using ReactiveUI;
using updock_example.Models;

namespace updock_example.ViewModels;

/// <summary>
/// サムネイルプレビューの状態を管理するViewModel
/// </summary>
public class ThumbnailViewModel : ViewModelBase
{
    private ThumbnailItem? _selectedThumbnail;
    private string _currentFolder = string.Empty;
    private readonly ThumbnailGenerator _thumbnailGenerator = new();

    /// <summary>
    /// サムネイル一覧
    /// </summary>
    public ObservableCollection<ThumbnailItem> Thumbnails { get; } = new();

    /// <summary>
    /// 選択されたサムネイル
    /// </summary>
    public ThumbnailItem? SelectedThumbnail
    {
        get => _selectedThumbnail;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedThumbnail, value);
            if (value != null)
            {
                // サムネイルが選択されたことを通知
                ThumbnailSelected?.Invoke(this, new ThumbnailSelectedEventArgs(value.ImageFile));
            }
        }
    }

    /// <summary>
    /// 現在のフォルダパス
    /// </summary>
    public string CurrentFolder
    {
        get => _currentFolder;
        set => this.RaiseAndSetIfChanged(ref _currentFolder, value);
    }

    /// <summary>
    /// サムネイル選択コマンド
    /// </summary>
    public ReactiveCommand<ThumbnailItem, Unit> SelectThumbnailCommand { get; }

    /// <summary>
    /// サムネイル選択イベント
    /// </summary>
    public event EventHandler<ThumbnailSelectedEventArgs>? ThumbnailSelected;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public ThumbnailViewModel()
    {
        // コマンドの初期化
        SelectThumbnailCommand = ReactiveCommand.Create<ThumbnailItem>(SelectThumbnail);
    }

    /// <summary>
    /// サムネイル一覧の更新
    /// </summary>
    /// <param name="folderPath">フォルダパス</param>
    public void UpdateThumbnails(string folderPath)
    {
        CurrentFolder = folderPath;
        Thumbnails.Clear();

        if (Directory.Exists(folderPath))
        {
            try
            {
                var files = Directory.GetFiles(folderPath);
                foreach (var file in files)
                {
                    // JPEGまたはPNGファイルのみを表示
                    var extension = Path.GetExtension(file).ToLower();
                    if (extension == ".jpg" || extension == ".jpeg" || extension == ".png")
                    {
                        var imageFile = new ImageFileModel(file);
                        var thumbnailImage = _thumbnailGenerator.GenerateThumbnail(file);
                        var thumbnailItem = new ThumbnailItem(imageFile, thumbnailImage);
                        Thumbnails.Add(thumbnailItem);
                    }
                }
            }
            catch
            {
                // サムネイルの更新に失敗した場合は何もしない
            }
        }
    }

    /// <summary>
    /// サムネイルを選択
    /// </summary>
    /// <param name="thumbnail">選択するサムネイル</param>
    private void SelectThumbnail(ThumbnailItem thumbnail)
    {
        SelectedThumbnail = thumbnail;
    }
}

/// <summary>
/// サムネイル選択イベントの引数
/// </summary>
public class ThumbnailSelectedEventArgs : EventArgs
{
    /// <summary>
    /// 画像ファイル
    /// </summary>
    public ImageFileModel ImageFile { get; }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="imageFile">画像ファイル</param>
    public ThumbnailSelectedEventArgs(ImageFileModel imageFile)
    {
        ImageFile = imageFile;
    }
}