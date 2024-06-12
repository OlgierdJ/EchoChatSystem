using MudBlazor;

namespace EchoWebapp.Client.Provider;

public class StringToColor
{
    public Color ConvertStringToColor(string s)
    {
        switch (s)
        {
            case "Success":
                return Color.Success;

            case "Warning":
                return Color.Warning;

            case "Error":
                return Color.Error;

            case "Dark":
                return Color.Default;

            default:
                return Color.Default;
        }

    }
}
