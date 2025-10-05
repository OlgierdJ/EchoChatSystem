namespace EInkTemplatingAPI.Services;

using EInkTemplatingAPI.Models;
using System.Collections.Concurrent;

public class ScreenProfileService
{
    private readonly ConcurrentDictionary<string, ScreenProfile> _profiles = new();


    public ScreenProfileService()
    {
        // Add your default profiles here. Add more as you get new hardware.
        _profiles["2.9-inch"] = new ScreenProfile { Name = "2.9-inch", Width = 296, Height = 128, DiagonalInches = 2.9 };
        _profiles["Kindle-6"] = new ScreenProfile { Name = "Kindle-6", Width = 1448, Height = 1072, DiagonalInches = 6.0 };
        _profiles["13.3-inch"] = new ScreenProfile { Name = "13.3-inch", Width = 2200, Height = 1650, DiagonalInches = 13.3 };
    }


    public ScreenProfile? GetProfileOrNull(string key)
    {
        _profiles.TryGetValue(key, out var p);
        return p;
    }


    public void RegisterProfile(string key, ScreenProfile profile) => _profiles[key] = profile;
}

