using System.Collections.ObjectModel;

namespace updock_example.Models;

/// <summary>
/// ファイルシステムのアイテム（ファイルまたはディレクトリ）を表すクラス
/// </summary>
public class FileSystemItem
{
    /// <summary>
    /// アイテム名
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// フルパス
    /// </summary>
    public string FullPath { get; set; } = string.Empty;
    
    /// <summary>
    /// ディレクトリかどうか
    /// </summary>
    public bool IsDirectory { get; set; }
    
    /// <summary>
    /// 子アイテム（ディレクトリの場合）
    /// </summary>
    public ObservableCollection<FileSystemItem> Children { get; set; } = new();
    
    /// <summary>
    /// 展開状態（ツリー表示用）
    /// </summary>
    public bool IsExpanded { get; set; }
    
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public FileSystemItem()
    {
    }
    
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="name">アイテム名</param>
    /// <param name="fullPath">フルパス</param>
    /// <param name="isDirectory">ディレクトリかどうか</param>
    public FileSystemItem(string name, string fullPath, bool isDirectory)
    {
        Name = name;
        FullPath = fullPath;
        IsDirectory = isDirectory;
    }
}