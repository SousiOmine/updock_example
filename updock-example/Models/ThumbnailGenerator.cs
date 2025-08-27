using System;
using System.IO;
using Avalonia.Media.Imaging;

namespace updock_example.Models;

/// <summary>
/// サムネイルを生成するクラス
/// </summary>
public class ThumbnailGenerator
{
    /// <summary>
    /// サムネイルのサイズ
    /// </summary>
    public Size ThumbnailSize { get; set; } = new Size(150, 150);
    
    /// <summary>
    /// 画像からサムネイルを生成
    /// </summary>
    /// <param name="imageFile">画像ファイル</param>
    /// <returns>サムネイル画像</returns>
    public Bitmap? GenerateThumbnail(ImageFileModel imageFile)
    {
        return GenerateThumbnail(imageFile.FilePath);
    }
    
    /// <summary>
    /// ファイルパスからサムネイルを生成
    /// </summary>
    /// <param name="filePath">ファイルパス</param>
    /// <returns>サムネイル画像</returns>
    public Bitmap? GenerateThumbnail(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
                return null;
            
            // 画像を読み込む
            using var originalStream = File.OpenRead(filePath);
            var originalBitmap = new Bitmap(originalStream);
            
            // サムネイルのサイズを計算
            var scale = Math.Min(
                (double)ThumbnailSize.Width / originalBitmap.PixelSize.Width,
                (double)ThumbnailSize.Height / originalBitmap.PixelSize.Height);
            
            var thumbnailWidth = (int)(originalBitmap.PixelSize.Width * scale);
            var thumbnailHeight = (int)(originalBitmap.PixelSize.Height * scale);
            
            // サムネイルを生成
            return originalBitmap.CreateScaledBitmap(
                new Avalonia.PixelSize(thumbnailWidth, thumbnailHeight));
        }
        catch
        {
            // サムネイルの生成に失敗した場合はnullを返す
            return null;
        }
    }
}