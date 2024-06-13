using Echo.Application.Contracts.Interfaces.Contracts;
using Echo.Domain.Shared.Entities.EchoCore.ServerCore.GeneralCore;
using Echo.Domain.Shared.Entities.EchoCore.ServerCore.GeneralCore.ManagementCore;

namespace Echo.Domain.Shared.Entities.EchoCore.ServerCore.ChannelCore.VoiceChannel;

public class ServerVoiceChannelMemberSettings : IDomainEntity
{
    public ulong ChannelId { get; set; } //where these settings belong
    public ulong AccountId { get; set; }
    public ulong ServerId { get; set; }

    public ServerVoiceChannel Channel { get; set; }
    public ServerProfile Profile { get; set; }
    public Server Server { get; set; } //ignore
    public ICollection<ServerVoiceChannelMemberPermission>? Permissions { get; set; }
}