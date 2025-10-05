using EInkTemplatingAPI.Models;
using EInkTemplatingAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PuppeteerSharp;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace EInkTemplatingAPI.Controllers;
[Route("api/[controller]")]
[ApiController]

public class RenderController : ControllerBase
{
    private readonly TemplateService _templateService;
    private readonly PuppeteerRenderService _renderService;
    private readonly ImageService _imageService;
    private readonly ScreenProfileService _screenService;


    public RenderController(
    TemplateService templateService,
    PuppeteerRenderService renderService,
    ImageService imageService,
    ScreenProfileService screenService)
    {
        _templateService = templateService;
        _renderService = renderService;
        _imageService = imageService;
        _screenService = screenService;
    }


    [HttpPost("product/{screenId}")]
    public async Task<IActionResult> RenderProductPng(string screenId, [FromBody] Product product)
    {
        var screen = _screenService.GetProfileOrNull(screenId);
        if (screen == null) return NotFound(new { error = "Unknown screen profile" });


        var html = _templateService.RenderProduct(product, screen);
        var pngBytes = await _renderService.RenderHtmlToPngAsync(html, screen);


        // Optional post processing: ensure exact pixel size and apply grayscale
        var processed = _imageService.ProcessForEInk(pngBytes, screen);


        return File(processed, "image/png");
    }


    [HttpPost("product/raw/{screenId}")]
    public async Task<IActionResult> RenderProductRaw(string screenId, [FromBody] Product product)
    {
        var screen = _screenService.GetProfileOrNull(screenId);
        if (screen == null) return NotFound(new { error = "Unknown screen profile" });


        var html = _templateService.RenderProduct(product, screen);
        var pngBytes = await _renderService.RenderHtmlToPngAsync(html, screen);


        var monoBuffer = _imageService.ConvertPngTo1BitBuffer(pngBytes, screen);


        // Raw buffer returned as application/octet-stream; device code will know packing
        return File(monoBuffer, "application/octet-stream");
    }

    [HttpPost("product/preview/{screenId}")]
    public async Task<IActionResult> RenderProductPreview(
    string screenId,
    [FromBody] Product product,
    [FromQuery] bool dither = false)
    {
        var screen = _screenService.GetProfileOrNull(screenId);
        if (screen == null)
            return NotFound(new { error = "Unknown screen profile" });

        var html = _templateService.RenderProduct(product, screen);
        var pngBytes = await _renderService.RenderHtmlToPngAsync(html, screen);

        byte[] buffer;

        if (dither)
        {
            buffer = _imageService.DitherTo1Bit(pngBytes, screen);
        }
        else
        {
            buffer = _imageService.ConvertPngTo1BitBuffer(pngBytes, screen);
        }

        // Convert the 1-bit buffer back into a PNG for preview
        using var previewImage = new Image<L8>(screen.Width, screen.Height);

        int stride = (screen.Width + 7) / 8;
        previewImage.ProcessPixelRows(accessor =>
        {
            for (int y = 0; y < screen.Height; y++)
            {
                var row = accessor.GetRowSpan(y);
                for (int x = 0; x < screen.Width; x++)
                {
                    int byteIndex = y * stride + (x >> 3);
                    int bitIndex = 7 - (x & 7);
                    bool isBlack = (buffer[byteIndex] & (1 << bitIndex)) != 0;
                    row[x].PackedValue = (byte)(isBlack ? 0 : 255); // 0 = black, 255 = white
                }
            }
        });

        using var ms = new MemoryStream();
        previewImage.SaveAsPng(ms);
        return File(ms.ToArray(), "image/png");
    }
}
