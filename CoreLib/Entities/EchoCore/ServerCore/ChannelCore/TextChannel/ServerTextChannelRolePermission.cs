using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.RoleCore;
using CoreLib.Interfaces.Contracts;

namespace CoreLib.Entities.EchoCore.ServerCore.ChannelCore
{
    public class ServerTextChannelRolePermission : IDomainEntity
    //Used for displaying state of serverpermission for a specific channel for a specific role
    {
        //combine foreign key channel and role.
        //pk is combination of channel, role and permission
        public ulong ChannelId { get; set; }
        public ulong RoleId { get; set; }
        public ulong PermissionId { get; set; }
        public bool? State { get; set; } //true = enabled, null = default, false = disabled
        public ServerTextChannelRole ChannelRole { get; set; } //cascade
        public ServerTextChannel Channel { get; set; } //ignore
        public ServerRole Role { get; set; } //ignore
        public ServerPermission Permission { get; set; } //cascade
    }
}