using Echo.Domain.Shared.Entities.Base;
using Echo.Domain.Shared.Entities.EchoCore.AccountCore;

namespace Echo.Domain.Shared.Entities.EchoCore.ApplicationCore.SettingsCore;

public class StreamerModeSettings : BaseEntity<ulong>
{
    //public ulong AccountSettingsId { get; set; }

    public bool EnableStreamerMode { get; set; }
    public bool AutomaticallyEnableAndDisableIfStreaming { get; set; }
    public bool HidePersonalInformation { get; set; }
    public bool HideInviteLinks { get; set; }
    public bool DisableSounds { get; set; }
    public bool DisableNotifications { get; set; }
    public bool HideEchoWindowFromScreenCapture { get; set; }

    public AccountSettings AccountSettings { get; set; }
}
