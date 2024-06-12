namespace CoreLib.DTO.EchoCore.UserCore.SettingsCore;

public interface IStreamerModeSettings
{
    bool AutomaticallyEnableAndDisableIfStreaming { get; set; }
    bool DisableNotifications { get; set; }
    bool DisableSounds { get; set; }
    bool EnableStreamerMode { get; set; }
    bool HideEchoWindowFromScreenCapture { get; set; }
    bool HideInviteLinks { get; set; }
    bool HidePersonalInformation { get; set; }
    ulong Id { get; set; }
}

public class StreamerModeSettingsDTO : IStreamerModeSettings
{
    public ulong Id { get; set; }

    public bool EnableStreamerMode { get; set; }
    public bool AutomaticallyEnableAndDisableIfStreaming { get; set; }
    public bool HidePersonalInformation { get; set; }
    public bool HideInviteLinks { get; set; }
    public bool DisableSounds { get; set; }
    public bool DisableNotifications { get; set; }
    public bool HideEchoWindowFromScreenCapture { get; set; }
}
