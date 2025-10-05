namespace EInkTemplatingAPI.Models;

public class ScreenProfile
{
    public string Name { get; set; } = "";
    public int Width { get; set; }
    public int Height { get; set; }
    public double DiagonalInches { get; set; }

    public double Ppi =>
        Math.Sqrt(Width * Width + Height * Height) / DiagonalInches;
}

