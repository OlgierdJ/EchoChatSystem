using CoreLib.DTO.Contracts;

namespace CoreLib.DTO.EchoCore.MiscCore.ModerationCore;


public class RoleMinimalWithPermissionsDTO : IRoleMinimal, IRoleMinimalWithPermissions<PermissionMinimalDTO>
{
    public ICollection<PermissionMinimalDTO> Permissions { get; set; }
    public ulong Id { get; set; }
    public string Name { get; set; }
}
