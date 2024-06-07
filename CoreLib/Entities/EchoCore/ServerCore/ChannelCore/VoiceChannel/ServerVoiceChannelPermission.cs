using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.RoleCore;
using CoreLib.Interfaces;

namespace CoreLib.Entities.EchoCore.ServerCore.ChannelCore
{
    public class ServerVoiceChannelPermission: IDomainEntity
    //used for mapping displayed permissions within a channel
    {
        //pk is combination of channel, and permission
        public ulong ChannelId { get; set; }
        public ulong PermissionId { get; set; }
        public ServerVoiceChannel Channel { get; set; } //cascade
        public ServerPermission Permission { get; set; } //cascade
    }
}