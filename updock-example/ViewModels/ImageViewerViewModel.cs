using System;
using Avalonia.Media.Imaging;
using ReactiveUI;
using updock_example.Models;

namespace updock_example.ViewModels;

/// <summary>
/// メイン画像表示の状態を管理するViewModel
/// </summary>
public class ImageViewerViewModel : ViewModelBase
{
    private Bitmap? _imageBitmap;
    private double _zoomFactor = 1.0;

    /// <summary>
    /// 表示する画像
    /// </summary>
    public Bitmap? ImageBitmap
    {
        get => _imageBitmap;
        set => this.RaiseAndSetIfChanged(ref _imageBitmap, value);
    }

    /// <summary>
    /// ズーム倍率
    /// </summary>
    public double ZoomFactor
    {
        get => _zoomFactor;
        set => this.RaiseAndSetIfChanged(ref _zoomFactor, value);
    }

    /// <summary>
    /// 画像の更新
    /// </summary>
    /// <param name="imageFile">画像ファイル</param>
    public void UpdateImage(ImageFileModel? imageFile)
    {
        if (imageFile != null)
        {
            imageFile.LoadImage();
            ImageBitmap = imageFile.ImageBitmap;
        }
        else
        {
            ImageBitmap = null;
        }
        
        // ズーム倍率をリセット
        ZoomFactor = 1.0;
    }

    /// <summary>
    /// ズームイン
    /// </summary>
    public void ZoomIn()
    {
        ZoomFactor *= 1.2;
    }

    /// <summary>
    /// ズームアウト
    /// </summary>
    public void ZoomOut()
    {
        ZoomFactor /= 1.2;
    }

    /// <summary>
    /// 実寸表示
    /// </summary>
    public void ZoomActualSize()
    {
        ZoomFactor = 1.0;
    }
}