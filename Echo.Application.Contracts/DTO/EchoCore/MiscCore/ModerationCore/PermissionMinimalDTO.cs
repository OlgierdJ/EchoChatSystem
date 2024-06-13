using Echo.Application.Contracts.DTO.Contracts;

namespace Echo.Application.Contracts.DTO.EchoCore.MiscCore.ModerationCore;

public class PermissionMinimalDTO : IPermissionMinimal
{
    public ulong Id { get; set; }
    public string Name { get; set; }
}
