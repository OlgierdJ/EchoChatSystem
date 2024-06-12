using CoreLib.Entities.EchoCore.ServerCore.GeneralCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.ManagementCore;
using CoreLib.Interfaces.Contracts;

namespace CoreLib.Entities.EchoCore.ServerCore.ChannelCore;

public class ServerTextChannelMemberSettings : IDomainEntity
{
    public ulong ChannelId { get; set; } //where these settings belong
    public ulong AccountId { get; set; }
    public ulong ServerId { get; set; }

    public ServerTextChannel Channel { get; set; }
    public ServerProfile Profile { get; set; }
    public Server Server { get; set; } //ignore
    public ICollection<ServerTextChannelMemberPermission>? Permissions { get; set; }
}