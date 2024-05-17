namespace CoreLib.DTO.EchoCore.UserCore.SettingsCore
{
    public class StreamerModeSettingsDTO
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
}
