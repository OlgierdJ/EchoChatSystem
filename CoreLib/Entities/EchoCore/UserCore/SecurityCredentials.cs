using CoreLib.Entities.Base;

namespace CoreLib.Entities.EchoCore.UserCore;

public class SecurityCredentials : BaseEntity<ulong>
{
    public ulong UserId { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] Salt { get; set; }
    public User User { get; set; }
}
