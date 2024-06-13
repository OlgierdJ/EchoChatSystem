using Echo.Application.Contracts.DTO.Contracts;

namespace Echo.Application.Contracts.DTO.EchoCore.MiscCore;


public class BadgeDTO : IBadge
{
    public ulong Id { get; set; }
    public int OrderingWeight { get; set; }
    public string Description { get; set; }
    public string IconURL { get; set; }
}