using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.Enums;

namespace CoreLib.Entities.EchoCore.ApplicationCore.Settings;

public class GameOverlaySettings : BaseEntity<ulong>
{
    //public ulong AccountSettingsId { get; set; }
    public bool EnableGameOverlay { get; set; }
    public string ToggleOverlayLockKeybind { get; set; } //Example "Shift + L"
    public AvatarSizeMode AvatarSizeMode { get; set; }
    public DisplayNamesMode DisplayNamesMode { get; set; }
    public DisplayUsersMode DisplayUsersMode { get; set; }
    public OverlayNotificationsPlacement OverlayNotificationsPlacement { get; set; }
    public bool ShowTextChatNotifications { get; set; }

    public AccountSettings AccountSettings { get; set; }
}
