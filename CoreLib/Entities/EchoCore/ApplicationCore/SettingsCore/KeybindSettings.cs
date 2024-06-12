using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;

namespace CoreLib.Entities.EchoCore.ApplicationCore.Settings;

public class KeybindSettings : BaseEntity<ulong>
{
    //public ulong AccountSettingsId { get; set; }
    public ICollection<Keybind>? Keybinds { get; set; }
    public AccountSettings AccountSettings { get; set; }
}
