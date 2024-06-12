using CoreLib.Entities.Enums;

namespace CoreLib.DTO.EchoCore.UserCore.SettingsCore;

public interface IGameOverlaySettings
{
    AvatarSizeMode AvatarSizeMode { get; set; }
    DisplayNamesMode DisplayNamesMode { get; set; }
    DisplayUsersMode DisplayUsersMode { get; set; }
    bool EnableGameOverlay { get; set; }
    ulong Id { get; set; }
    OverlayNotificationsPlacement OverlayNotificationsPlacement { get; set; }
    bool ShowTextChatNotifications { get; set; }
    string ToggleOverlayLockKeybind { get; set; }
}

public class GameOverlaySettingsDTO : IGameOverlaySettings
{
    public ulong Id { get; set; }
    public bool EnableGameOverlay { get; set; }
    public string ToggleOverlayLockKeybind { get; set; } //Example "Shift + L"
    public AvatarSizeMode AvatarSizeMode { get; set; }
    public DisplayNamesMode DisplayNamesMode { get; set; }
    public DisplayUsersMode DisplayUsersMode { get; set; }
    public OverlayNotificationsPlacement OverlayNotificationsPlacement { get; set; }
    public bool ShowTextChatNotifications { get; set; }
}
