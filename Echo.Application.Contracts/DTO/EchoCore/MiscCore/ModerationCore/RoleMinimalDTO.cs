using Echo.Application.Contracts.DTO.Contracts;

namespace Echo.Application.Contracts.DTO.EchoCore.MiscCore.ModerationCore;

public class RoleMinimalDTO : IRoleMinimal
{
    public ulong Id { get; set; }
    public string Name { get; set; }
}
