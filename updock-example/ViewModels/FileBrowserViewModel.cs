using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reactive;
using ReactiveUI;
using updock_example.Models;

namespace updock_example.ViewModels;

/// <summary>
/// ファイルブラウザの状態を管理するViewModel
/// </summary>
public class FileBrowserViewModel : ViewModelBase
{
    private FileSystemItem? _selectedItem;
    private string _rootPath = string.Empty;

    /// <summary>
    /// ルートパス
    /// </summary>
    public string RootPath
    {
        get => _rootPath;
        set => this.RaiseAndSetIfChanged(ref _rootPath, value);
    }

    /// <summary>
    /// ツリーアイテム一覧
    /// </summary>
    public ObservableCollection<FileSystemItem> Items { get; } = new();

    /// <summary>
    /// 選択されたアイテム
    /// </summary>
    public FileSystemItem? SelectedItem
    {
        get => _selectedItem;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedItem, value);
            if (value != null && !value.IsDirectory)
            {
                // 画像ファイルが選択されたことを通知
                FileSelected?.Invoke(this, new FileSelectedEventArgs(value.FullPath));
            }
        }
    }

    /// <summary>
    /// フォルダ展開コマンド
    /// </summary>
    public ReactiveCommand<FileSystemItem, Unit> ExpandFolderCommand { get; }

    /// <summary>
    /// ファイル選択コマンド
    /// </summary>
    public ReactiveCommand<FileSystemItem, Unit> SelectFileCommand { get; }

    /// <summary>
    /// ファイル選択イベント
    /// </summary>
    public event EventHandler<FileSelectedEventArgs>? FileSelected;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public FileBrowserViewModel()
    {
        // コマンドの初期化
        ExpandFolderCommand = ReactiveCommand.Create<FileSystemItem>(ExpandFolder);
        SelectFileCommand = ReactiveCommand.Create<FileSystemItem>(SelectFile);
    }

    /// <summary>
    /// ルートディレクトリを設定
    /// </summary>
    /// <param name="path">ルートディレクトリのパス</param>
    public void SetRootDirectory(string path)
    {
        RootPath = path;
        LoadRootItems();
    }

    /// <summary>
    /// ルートアイテムを読み込む
    /// </summary>
    private void LoadRootItems()
    {
        Items.Clear();

        if (Directory.Exists(RootPath))
        {
            try
            {
                var directories = Directory.GetDirectories(RootPath);
                foreach (var directory in directories)
                {
                    var item = new FileSystemItem
                    {
                        Name = Path.GetFileName(directory),
                        FullPath = directory,
                        IsDirectory = true
                    };
                    Items.Add(item);
                }

                var files = Directory.GetFiles(RootPath);
                foreach (var file in files)
                {
                    // JPEGまたはPNGファイルのみを表示
                    var extension = Path.GetExtension(file).ToLower();
                    if (extension == ".jpg" || extension == ".jpeg" || extension == ".png")
                    {
                        var item = new FileSystemItem
                        {
                            Name = Path.GetFileName(file),
                            FullPath = file,
                            IsDirectory = false
                        };
                        Items.Add(item);
                    }
                }
            }
            catch
            {
                // ディレクトリの読み込みに失敗した場合は何もしない
            }
        }
    }

    /// <summary>
    /// フォルダを展開
    /// </summary>
    /// <param name="item">展開するフォルダアイテム</param>
    private void ExpandFolder(FileSystemItem item)
    {
        if (!item.IsDirectory || item.IsExpanded)
            return;

        item.Children.Clear();

        try
        {
            var directories = Directory.GetDirectories(item.FullPath);
            foreach (var directory in directories)
            {
                var childItem = new FileSystemItem
                {
                    Name = Path.GetFileName(directory),
                    FullPath = directory,
                    IsDirectory = true
                };
                item.Children.Add(childItem);
            }

            var files = Directory.GetFiles(item.FullPath);
            foreach (var file in files)
            {
                // JPEGまたはPNGファイルのみを表示
                var extension = Path.GetExtension(file).ToLower();
                if (extension == ".jpg" || extension == ".jpeg" || extension == ".png")
                {
                    var childItem = new FileSystemItem
                    {
                        Name = Path.GetFileName(file),
                        FullPath = file,
                        IsDirectory = false
                    };
                    item.Children.Add(childItem);
                }
            }

            item.IsExpanded = true;
        }
        catch
        {
            // フォルダの展開に失敗した場合は何もしない
        }
    }

    /// <summary>
    /// ファイルを選択
    /// </summary>
    /// <param name="item">選択するアイテム</param>
    private void SelectFile(FileSystemItem item)
    {
        SelectedItem = item;
    }
}

/// <summary>
/// ファイル選択イベントの引数
/// </summary>
public class FileSelectedEventArgs : EventArgs
{
    /// <summary>
    /// ファイルパス
    /// </summary>
    public string FilePath { get; }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="filePath">ファイルパス</param>
    public FileSelectedEventArgs(string filePath)
    {
        FilePath = filePath;
    }
}