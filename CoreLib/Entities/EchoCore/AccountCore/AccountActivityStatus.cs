using CoreLib.Entities.Base;

namespace CoreLib.Entities.EchoCore.AccountCore;

public class AccountActivityStatus : BaseEntity<byte> //maybe review 
                                                      //accounts share the same few activity status (offline, idle, online, do not disturb, and invisible)
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string Icon { get; set; }
    public string IconColor { get; set; }
    public ICollection<Account>? Accounts { get; set; }
}
