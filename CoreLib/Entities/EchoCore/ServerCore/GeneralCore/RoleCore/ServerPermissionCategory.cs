using CoreLib.Entities.Base;

namespace CoreLib.Entities.EchoCore.ServerCore.GeneralCore.RoleCore;

public class ServerPermissionCategory : BaseEntity<byte>
{
    public string Name { get; set; }
    public string? Description { get; set; }

    public ICollection<ServerPermission>? Permissions { get; set; }
}
