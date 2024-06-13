using Echo.Application.Contracts.Interfaces.Contracts;
using Echo.Domain.Shared.Entities.EchoCore.ServerCore.GeneralCore.RoleCore;

namespace Echo.Domain.Shared.Entities.EchoCore.ServerCore.ChannelCore.TextChannel;

public class ServerTextChannelPermission : IDomainEntity
//used for mapping displayed permissions within a channelcategory
{
    //pk is combination of channel, and permission
    public ulong ChannelId { get; set; }
    public ulong PermissionId { get; set; }
    public ServerTextChannel Channel { get; set; } //cascade
    public ServerPermission Permission { get; set; } //cascade
}