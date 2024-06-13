using Echo.Application.Contracts.Interfaces.Contracts;
using Echo.Domain.Shared.Entities.Base;

namespace Echo.Domain.Shared.Entities.EchoCore.ApplicationCore;

public class Permission : BaseEntity<ulong>, IRolePermission<Role>
{
    public ICollection<Role>? Roles { get; set; }
    public string Name { get; set; }
}
