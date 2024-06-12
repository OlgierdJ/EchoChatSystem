using CoreLib.Entities.Base;

namespace CoreLib.Entities.EchoCore.ApplicationCore;

public class RolePermission : BaseEntity<ulong>
{
    public ulong RoleId { get; set; }
    public ulong PermissionId { get; set; }
    public Role Role { get; set; }
    public Permission Permission { get; set; }
}
