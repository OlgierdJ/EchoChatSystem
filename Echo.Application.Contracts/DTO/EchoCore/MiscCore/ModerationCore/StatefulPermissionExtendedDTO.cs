using Echo.Application.Contracts.DTO.Contracts;

namespace Echo.Application.Contracts.DTO.EchoCore.MiscCore.ModerationCore;


public class StatefulPermissionExtendedDTO : IStatefulPermissionExtended
{
    public string? GroupingName { get; set; } //taken from category
    public string? Description { get; set; }
    public ulong Id { get; set; }
    public string Name { get; set; }
    public bool? State { get; set; }
}
