using System;
using System.IO;
using Avalonia.Media.Imaging;

namespace updock_example.Models;

/// <summary>
/// 画像ファイルの情報を管理するクラス
/// </summary>
public class ImageFileModel
{
    /// <summary>
    /// ファイルパス
    /// </summary>
    public string FilePath { get; set; } = string.Empty;
    
    /// <summary>
    /// ファイル名
    /// </summary>
    public string FileName { get; set; } = string.Empty;
    
    /// <summary>
    /// ファイルサイズ
    /// </summary>
    public long FileSize { get; set; }
    
    /// <summary>
    /// 画像の解像度
    /// </summary>
    public Size ImageResolution { get; set; }
    
    /// <summary>
    /// 最終更新日時
    /// </summary>
    public DateTime LastModified { get; set; }
    
    /// <summary>
    /// 画像のビットマップ
    /// </summary>
    public Bitmap? ImageBitmap { get; set; }
    
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public ImageFileModel()
    {
    }
    
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="filePath">ファイルパス</param>
    public ImageFileModel(string filePath)
    {
        FilePath = filePath;
        FileName = Path.GetFileName(filePath);
        
        if (File.Exists(filePath))
        {
            var fileInfo = new FileInfo(filePath);
            FileSize = fileInfo.Length;
            LastModified = fileInfo.LastWriteTime;
            
            LoadMetadata();
        }
    }
    
    /// <summary>
    /// メタデータの読み込み
    /// </summary>
    private void LoadMetadata()
    {
        try
        {
            if (File.Exists(FilePath))
            {
                using var stream = File.OpenRead(FilePath);
                var bitmap = new Bitmap(stream);
                ImageResolution = new Size(bitmap.PixelSize.Width, bitmap.PixelSize.Height);
                ImageBitmap = bitmap;
            }
        }
        catch
        {
            // メタデータの読み込みに失敗した場合はデフォルト値を使用
            ImageResolution = new Size(0, 0);
        }
    }
    
    /// <summary>
    /// 画像の読み込み
    /// </summary>
    public void LoadImage()
    {
        try
        {
            if (File.Exists(FilePath))
            {
                ImageBitmap = new Bitmap(FilePath);
            }
        }
        catch
        {
            // 画像の読み込みに失敗した場合はnullを設定
            ImageBitmap = null;
        }
    }
}

/// <summary>
/// サイズを表す構造体
/// </summary>
public struct Size
{
    /// <summary>
    /// 幅
    /// </summary>
    public int Width { get; set; }
    
    /// <summary>
    /// 高さ
    /// </summary>
    public int Height { get; set; }
    
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="width">幅</param>
    /// <param name="height">高さ</param>
    public Size(int width, int height)
    {
        Width = width;
        Height = height;
    }
    
    /// <summary>
    /// 文字列表現を返す
    /// </summary>
    /// <returns>サイズの文字列表現</returns>
    public override string ToString()
    {
        return $"{Width} x {Height}";
    }
}