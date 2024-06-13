using Echo.Application.Contracts.DTO.EchoCore.MiscCore.AppearanceCore;
using Echo.Application.Contracts.Enums;

namespace Echo.Application.Contracts.DTO.EchoCore.UserCore.SettingsCore;

public interface IAppearanceSettings
{
    bool DarkSideBar { get; set; }
    ulong Id { get; set; }
    string InAppIcon { get; set; }
    MessageDisplayMode MessageDisplayMode { get; set; }
    byte PixelChatFontScale { get; set; }
    byte PixelSpaceBetweenMessageGroupsScale { get; set; }
    bool ShowAvatarsInCompactMode { get; set; }
    ThemeDTO Theme { get; set; }
    uint ThemeId { get; set; }
    byte ZoomLevel { get; set; }
}

public class AppearanceSettingsDTO : IAppearanceSettings
{
    public ulong Id { get; set; }
    public uint ThemeId { get; set; }
    public string InAppIcon { get; set; }
    public bool DarkSideBar { get; set; }
    public MessageDisplayMode MessageDisplayMode { get; set; }
    public bool ShowAvatarsInCompactMode { get; set; }
    public byte PixelChatFontScale { get; set; }
    public byte PixelSpaceBetweenMessageGroupsScale { get; set; }
    public byte ZoomLevel { get; set; }

    public ThemeDTO Theme { get; set; }
}
