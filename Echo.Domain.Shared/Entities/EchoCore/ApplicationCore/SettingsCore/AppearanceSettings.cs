using Echo.Application.Contracts.Enums;
using Echo.Domain.Shared.Entities.Base;
using Echo.Domain.Shared.Entities.EchoCore.AccountCore;
using Echo.Domain.Shared.Entities.EchoCore.ApplicationCore;

namespace Echo.Domain.Shared.Entities.EchoCore.ApplicationCore.SettingsCore;

public class AppearanceSettings : BaseEntity<ulong>
{
    //public ulong AccountSettingsId { get; set; }
    public uint ThemeId { get; set; }
    public string InAppIcon { get; set; }
    public bool DarkSideBar { get; set; }
    public MessageDisplayMode MessageDisplayMode { get; set; }
    public bool ShowAvatarsInCompactMode { get; set; }
    public byte PixelChatFontScale { get; set; }
    public byte PixelSpaceBetweenMessageGroupsScale { get; set; }
    public byte ZoomLevel { get; set; }

    public Theme Theme { get; set; }
    public AccountSettings AccountSettings { get; set; }
}
