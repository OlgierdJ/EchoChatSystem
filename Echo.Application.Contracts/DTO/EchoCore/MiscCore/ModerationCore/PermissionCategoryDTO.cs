using Echo.Application.Contracts.DTO.Contracts;

namespace Echo.Application.Contracts.DTO.EchoCore.MiscCore.ModerationCore;


public class PermissionCategoryDTO : IPermissionCategory<PermissionMinimalDTO>
//used to display possible permissions for a role when editing them.
{
    public byte Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public ICollection<PermissionMinimalDTO>? Permissions { get; set; }
}
