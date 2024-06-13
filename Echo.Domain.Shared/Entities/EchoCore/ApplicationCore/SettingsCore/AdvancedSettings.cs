using Echo.Domain.Shared.Entities.Base;
using Echo.Domain.Shared.Entities.EchoCore.AccountCore;

namespace Echo.Domain.Shared.Entities.EchoCore.ApplicationCore.SettingsCore;

public class AdvancedSettings : BaseEntity<ulong>
{
    //public ulong AccountSettingsId { get; set; }
    public bool DeveloperMode { get; set; }
    public bool UseHardwareAccelerationToMakeEchoSmoother { get; set; }
    public bool AutoNavigateServerHome { get; set; }
    public AccountSettings AccountSettings { get; set; }
}
