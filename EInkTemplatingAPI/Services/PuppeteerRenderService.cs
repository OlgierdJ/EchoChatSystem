using EInkTemplatingAPI.Models;
using PuppeteerSharp;
using System.IO.Compression;
using System.Runtime.InteropServices;

namespace EInkTemplatingAPI.Services;

public class PuppeteerRenderService
{


    public PuppeteerRenderService()
    {
        // lazy download will be handled on first render
    }


    private static bool _downloaded = false;
    private static readonly SemaphoreSlim _downloadLock = new(1, 1);

    private async Task EnsureBrowserDownloadedAsync()
    {
        if (_downloaded) return;

        await _downloadLock.WaitAsync();
        try
        {
            if (_downloaded) return; // double-check inside lock

            // Define the desired Chrome revision for PuppeteerSharp 20.2.2
            string revision = "122.0.6261.57"; // Replace with the actual revision number

            string platform = GetPlatformString();

            string downloadUrl = $"https://storage.googleapis.com/chromium-browser-snapshots/{platform}/{revision}/chrome-{platform}.zip";
            string tempZip = Path.Combine(Path.GetTempPath(), $"chromium-{platform}.zip");
            string extractPath = Path.Combine(Path.GetTempPath(), $"chromium-{platform}");

            if (!Directory.Exists(extractPath))
                Directory.CreateDirectory(extractPath);

            using (var client = new HttpClient())
            {
                Console.WriteLine($"Downloading Chromium {revision} for {platform}...");
                var response = await client.GetAsync(downloadUrl);
                response.EnsureSuccessStatusCode();

                await using (var fs = new FileStream(tempZip, FileMode.Create, FileAccess.Write))
                {
                    await response.Content.CopyToAsync(fs);
                }
            }

            Console.WriteLine($"Extracting Chromium to {extractPath}...");
            ZipFile.ExtractToDirectory(tempZip, extractPath, true);
            File.Delete(tempZip);

            _downloaded = true;
            Console.WriteLine("Chromium downloaded and ready.");
        }
        finally
        {
            _downloadLock.Release();
        }
    }

    private string GetPlatformString()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return RuntimeInformation.ProcessArchitecture == Architecture.X64
                ? "Win_x64"
                : "Win_Arm64";
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            return RuntimeInformation.ProcessArchitecture == Architecture.X64
                ? "Linux_x64"
                : "Linux_Arm64";
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            return RuntimeInformation.ProcessArchitecture == Architecture.X64
                ? "Mac"
                : "Mac_Arm64";
        }
        else
        {
            throw new PlatformNotSupportedException("Unsupported OS platform");
        }
    }




    public async Task<byte[]> RenderHtmlToPngAsync(string html, ScreenProfile screen)
    {
        await EnsureBrowserDownloadedAsync();


        var launchOptions = new LaunchOptions
        {
            Headless = true,
            Args = new[] { "--no-sandbox", "--disable-setuid-sandbox" }
        };


        await using var browser = await Puppeteer.LaunchAsync(launchOptions);
        await using var page = await browser.NewPageAsync();


        await page.SetViewportAsync(new ViewPortOptions
        {
            Width = screen.Width,
            Height = screen.Height,
            DeviceScaleFactor = 1
        });


        await page.SetContentAsync(html, new NavigationOptions { WaitUntil = new[] { WaitUntilNavigation.Load } });


        var bytes = await page.ScreenshotDataAsync(new ScreenshotOptions { Type = ScreenshotType.Png, FullPage = false, OmitBackground = false });


        return bytes;
    }
}
