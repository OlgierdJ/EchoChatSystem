using Echo.Application.Contracts.Interfaces.Contracts;
using Echo.Domain.Shared.Entities.Base;

namespace Echo.Domain.Shared.Entities.EchoCore.ServerCore.ChannelCore.TextChannel;

public class ServerTextChannelMessageAttachment : BaseEntity<ulong>, IMessageAttachment<ServerTextChannelMessage, ulong>
{
    public ulong MessageId { get; set; }
    public string FileLocationURL { get; set; }
    public string FileName { get; set; }
    public string? Description { get; set; }
    public ServerTextChannelMessage Message { get; set; }
}