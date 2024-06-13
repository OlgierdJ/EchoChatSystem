using Echo.Domain.Shared.Entities.Base;
using Echo.Domain.Shared.Entities.EchoCore.AccountCore;
using Echo.Domain.Shared.Entities.EchoCore.ApplicationCore;

namespace Echo.Domain.Shared.Entities.EchoCore.ApplicationCore.SettingsCore;

public class KeybindSettings : BaseEntity<ulong>
{
    //public ulong AccountSettingsId { get; set; }
    public ICollection<Keybind>? Keybinds { get; set; }
    public AccountSettings AccountSettings { get; set; }
}
