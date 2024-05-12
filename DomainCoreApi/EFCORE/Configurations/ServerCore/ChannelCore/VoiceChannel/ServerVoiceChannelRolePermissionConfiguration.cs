using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.RoleCore;
using CoreLib.Entities.Enums;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.ChannelCore
{
    public class ServerVoiceChannelRolePermissionConfiguration
    //Used for displaying state of serverpermission for a specific channel for a specific role
    {
        //combine foreign key channel and role.
        //pk is combination of channel, role and permission
        public ulong ChannelId { get; set; }
        public ulong RoleId { get; set; }
        public ulong PermissionId { get; set; }
        public bool? State { get; set; } //true = enabled, null = default, false = disabled
        public ServerVoiceChannelRoleConfiguration ChannelRole { get; set; } //cascade
        public ServerVoiceChannelConfiguration Channel { get; set; } //ignore
        public ServerRole Role { get; set; } //ignore
        public ServerPermission Permission { get; set; } //cascade
    }
}