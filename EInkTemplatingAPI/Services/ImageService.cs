namespace EInkTemplatingAPI.Services;
using EInkTemplatingAPI.Models;


using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.IO;


public class ImageService
{
    public byte[] ProcessForEInk(byte[] pngBytes, ScreenProfile screen)
    {
        using var image = Image.Load<Rgba32>(pngBytes);

        image.Mutate(x => x
            .Resize(screen.Width, screen.Height)
            .Grayscale());

        using var ms = new MemoryStream();
        image.SaveAsPng(ms, new PngEncoder { ColorType = PngColorType.Grayscale });
        return ms.ToArray();
    }

    public byte[] ConvertPngTo1BitBuffer(byte[] pngBytes, ScreenProfile screen)
    {
        using var image = Image.Load<Rgba32>(pngBytes);
        image.Mutate(x => x
            .Resize(screen.Width, screen.Height)
            .Grayscale());

        int width = screen.Width;
        int height = screen.Height;
        int stride = (width + 7) / 8;
        var buffer = new byte[stride * height];

        image.ProcessPixelRows(accessor =>
        {
            for (int y = 0; y < height; y++)
            {
                var row = accessor.GetRowSpan(y);
                for (int x = 0; x < width; x++)
                {
                    var pixel = row[x];
                    int luminance = (pixel.R + pixel.G + pixel.B) / 3;
                    bool isBlack = luminance < 128;

                    if (isBlack)
                        buffer[y * stride + (x >> 3)] |= (byte)(0x80 >> (x & 7));
                }
            }
        });

        return buffer;
    }

    public byte[] DitherTo1Bit(byte[] pngBytes, ScreenProfile screen)
    {
        using var image = Image.Load<Rgba32>(pngBytes);
        image.Mutate(x => x
            .Resize(screen.Width, screen.Height)
            .Grayscale());

        int width = screen.Width;
        int height = screen.Height;
        int stride = (width + 7) / 8;
        var buffer = new byte[stride * height];
        var lum = new float[height, width];

        image.ProcessPixelRows(accessor =>
        {
            for (int y = 0; y < height; y++)
            {
                var row = accessor.GetRowSpan(y);
                for (int x = 0; x < width; x++)
                    lum[y, x] = row[x].R / 255f;
            }
        });

        // Floyd–Steinberg dithering
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float oldPixel = lum[y, x];
                float newPixel = oldPixel < 0.5f ? 0f : 1f;
                float error = oldPixel - newPixel;

                if (newPixel == 0f)
                    buffer[y * stride + (x >> 3)] |= (byte)(0x80 >> (x & 7));

                if (x + 1 < width) lum[y, x + 1] += error * 7 / 16f;
                if (y + 1 < height)
                {
                    if (x > 0) lum[y + 1, x - 1] += error * 3 / 16f;
                    lum[y + 1, x] += error * 5 / 16f;
                    if (x + 1 < width) lum[y + 1, x + 1] += error * 1 / 16f;
                }
            }
        }

        return buffer;
    }
}