using Echo.Application.Contracts.Interfaces.Contracts;
using Echo.Domain.Shared.Entities.EchoCore.ServerCore.GeneralCore.RoleCore;

namespace Echo.Domain.Shared.Entities.EchoCore.ServerCore.ChannelCore.VoiceChannel;

public class ServerVoiceChannelRole : IDomainEntity
{
    //channelcategory owner
    public ulong ChannelCategoryId { get; set; }
    //base role
    public ulong RoleId { get; set; }

    public ServerVoiceChannel Channel { get; set; }
    public ServerRole Role { get; set; }
    //independent permissions from the global permissions in server.
    //(these permissions are weighed more than the global server permissions except for serveradmin)
    public ICollection<ServerVoiceChannelRolePermission>? Permissions { get; set; }
}
