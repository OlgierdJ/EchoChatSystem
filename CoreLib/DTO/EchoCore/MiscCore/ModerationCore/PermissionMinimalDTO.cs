using CoreLib.DTO.Contracts;

namespace CoreLib.DTO.EchoCore.MiscCore.ModerationCore;

public class PermissionMinimalDTO : IPermissionMinimal
{
    public ulong Id { get; set; }
    public string Name { get; set; }
}
