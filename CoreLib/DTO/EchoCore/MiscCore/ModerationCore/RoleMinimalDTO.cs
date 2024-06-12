using CoreLib.DTO.Contracts;

namespace CoreLib.DTO.EchoCore.MiscCore.ModerationCore;

public class RoleMinimalDTO : IRoleMinimal
{
    public ulong Id { get; set; }
    public string Name { get; set; }
}
