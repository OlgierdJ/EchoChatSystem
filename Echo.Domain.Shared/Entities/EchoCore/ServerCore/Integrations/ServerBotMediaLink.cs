using Echo.Application.Contracts.Interfaces.Contracts;
using Echo.Domain.Shared.Entities.Base;

namespace Echo.Domain.Shared.Entities.EchoCore.ServerCore.Integrations;

public class ServerBotMediaLink : BaseEntity<ulong>, IMediaLink<ServerBot, ulong>
{
    public ulong OwnerId { get; set; }
    public string URL { get; set; }
    public string? IconURL { get; set; }
    public uint Importance { get; set; }
    public ServerBot Owner { get; set; }
}