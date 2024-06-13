using Echo.Domain.Shared.Entities.Base;
using Echo.Domain.Shared.Entities.EchoCore.AccountCore;

namespace Echo.Domain.Shared.Entities.EchoCore.ApplicationCore.SettingsCore;

public class WindowSettings : BaseEntity<ulong>
{
    //public ulong AccountSettingsId { get; set; }
    public bool OpenEchoOnPCStartup { get; set; }
    public bool StartMinimized { get; set; }
    public bool MinimizeOnClose { get; set; }

    public AccountSettings AccountSettings { get; set; }
}
