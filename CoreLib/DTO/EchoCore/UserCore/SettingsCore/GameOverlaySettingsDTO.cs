using CoreLib.Entities.Enums;

namespace CoreLib.DTO.EchoCore.UserCore.SettingsCore
{
    public class GameOverlaySettingsDTO
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
}
