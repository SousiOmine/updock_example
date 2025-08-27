using Avalonia.Media.Imaging;

namespace updock_example.Models;

/// <summary>
/// サムネイル表示用のアイテムを表すクラス
/// </summary>
public class ThumbnailItem
{
    /// <summary>
    /// 元の画像ファイル
    /// </summary>
    public ImageFileModel ImageFile { get; set; }
    
    /// <summary>
    /// サムネイル画像
    /// </summary>
    public Bitmap? ThumbnailImage { get; set; }
    
    /// <summary>
    /// ファイル名
    /// </summary>
    public string FileName { get; set; }
    
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="imageFile">元の画像ファイル</param>
    /// <param name="thumbnailImage">サムネイル画像</param>
    public ThumbnailItem(ImageFileModel imageFile, Bitmap? thumbnailImage)
    {
        ImageFile = imageFile;
        ThumbnailImage = thumbnailImage;
        FileName = imageFile.FileName;
    }
}