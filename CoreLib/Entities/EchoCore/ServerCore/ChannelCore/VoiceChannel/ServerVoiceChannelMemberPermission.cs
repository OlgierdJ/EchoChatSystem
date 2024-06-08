using CoreLib.Entities.EchoCore.ServerCore.GeneralCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.ManagementCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.RoleCore;
using CoreLib.Interfaces.Contracts;

namespace CoreLib.Entities.EchoCore.ServerCore.ChannelCore
{
    public class ServerVoiceChannelMemberPermission : IDomainEntity
    //Used for displaying state of serverpermission for a specific channel for a specific member
    {
        //combine foreign key profile and role.
        //pk is combination of channel, role and permission
        public ulong ChannelId { get; set; }
        public ulong AccountId { get; set; }
        public ulong ServerId { get; set; }
        public ulong PermissionId { get; set; }
        public bool? State { get; set; } //true = enabled, null = default, false = disabled
        public ServerVoiceChannelMemberSettings MemberSettings { get; set; } //cascade
        public ServerVoiceChannel Channel { get; set; } //ignore
        public ServerProfile Profile { get; set; } //ignore
        public Server Server { get; set; } //ignore
        public ServerPermission Permission { get; set; } //cascade
    }
}
