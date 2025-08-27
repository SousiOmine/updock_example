using ReactiveUI;
using updock_example.Models;

namespace updock_example.ViewModels;

/// <summary>
/// 画像プロパティの状態を管理するViewModel
/// </summary>
public class ImagePropertiesViewModel : ViewModelBase
{
    private string _fileName = string.Empty;
    private string _fileSize = string.Empty;
    private string _imageResolution = string.Empty;
    private string _lastModified = string.Empty;

    /// <summary>
    /// ファイル名
    /// </summary>
    public string FileName
    {
        get => _fileName;
        set => this.RaiseAndSetIfChanged(ref _fileName, value);
    }

    /// <summary>
    /// ファイルサイズ
    /// </summary>
    public string FileSize
    {
        get => _fileSize;
        set => this.RaiseAndSetIfChanged(ref _fileSize, value);
    }

    /// <summary>
    /// 画像解像度
    /// </summary>
    public string ImageResolution
    {
        get => _imageResolution;
        set => this.RaiseAndSetIfChanged(ref _imageResolution, value);
    }

    /// <summary>
    /// 最終更新日時
    /// </summary>
    public string LastModified
    {
        get => _lastModified;
        set => this.RaiseAndSetIfChanged(ref _lastModified, value);
    }

    /// <summary>
    /// 表示する画像情報
    /// </summary>
    public ImageFileModel? ImageInfo { get; private set; }

    /// <summary>
    /// 画像情報の更新
    /// </summary>
    /// <param name="imageFile">画像ファイル</param>
    public void UpdateImageInfo(ImageFileModel? imageFile)
    {
        ImageInfo = imageFile;

        if (imageFile != null)
        {
            FileName = imageFile.FileName;
            FileSize = FormatFileSize(imageFile.FileSize);
            ImageResolution = imageFile.ImageResolution.ToString();
            LastModified = imageFile.LastModified.ToString("yyyy/MM/dd HH:mm:ss");
        }
        else
        {
            FileName = string.Empty;
            FileSize = string.Empty;
            ImageResolution = string.Empty;
            LastModified = string.Empty;
        }
    }

    /// <summary>
    /// ファイルサイズをフォーマット
    /// </summary>
    /// <param name="size">ファイルサイズ（バイト）</param>
    /// <returns>フォーマットされたファイルサイズ</returns>
    private string FormatFileSize(long size)
    {
        string[] units = { "B", "KB", "MB", "GB" };
        double fileSize = size;
        int unitIndex = 0;

        while (fileSize >= 1024 && unitIndex < units.Length - 1)
        {
            fileSize /= 1024;
            unitIndex++;
        }

        return $"{fileSize:0.##} {units[unitIndex]}";
    }
}