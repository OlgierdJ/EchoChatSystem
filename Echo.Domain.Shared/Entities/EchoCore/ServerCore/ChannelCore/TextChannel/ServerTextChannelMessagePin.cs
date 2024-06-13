using Echo.Application.Contracts.Interfaces.Contracts;

namespace Echo.Domain.Shared.Entities.EchoCore.ServerCore.ChannelCore.TextChannel;

public class ServerTextChannelMessagePin : IMessagePin<ServerTextChannelMessage, ulong, ServerTextChannel, ulong>, IDomainEntity
{
    public ulong PinboardId { get; set; }
    public ulong MessageId { get; set; }
    public ServerTextChannel Pinboard { get; set; }
    public ServerTextChannelMessage Message { get; set; }
}