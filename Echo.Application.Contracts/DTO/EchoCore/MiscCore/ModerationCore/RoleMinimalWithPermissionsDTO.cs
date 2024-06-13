using Echo.Application.Contracts.DTO.Contracts;

namespace Echo.Application.Contracts.DTO.EchoCore.MiscCore.ModerationCore;


public class RoleMinimalWithPermissionsDTO : IRoleMinimal, IRoleMinimalWithPermissions<PermissionMinimalDTO>
{
    public ICollection<PermissionMinimalDTO> Permissions { get; set; }
    public ulong Id { get; set; }
    public string Name { get; set; }
}
