using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore.Category;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore.VoiceChannel;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.SettingsCore;
using CoreLib.Entities.Enums;

namespace CoreLib.Entities.EchoCore.ServerCore.ChannelCore
{
    public class ServerVoiceChannel : BaseChannel<ulong, ServerChannelCategory, ulong, Server, ulong>
    {
        public int OrderWeight { get; set; }
        public uint RegionId { get; set; }
        public uint BitRate { get; set; }
        public VideoQualityMode VideoQuality { get; set; }
        public uint UserLimit { get; set; } //default 0 = unlimited, max99 (maybe some other max cause server stability via signalr)
        public ServerRegion Region { get; set; } //this defines the hub server you connect to on voice join.
        public ServerSettings? ServerSettings { get; set; } //mapped through inactivechannel
        public ICollection<AccountServerVoiceChannelMute>? Muters { get; set; } 
        public ICollection<ServerVoiceInvite>? VoiceInvites { get; set; }
        public ICollection<ServerVoiceChannelPermission>? AllowedPermissions { get; set; } //related permissions used for creating subset of all permissions showed to the user
        public ICollection<ServerVoiceChannelRole>? AllowedRoles { get; set; } //all roles with specific defined permissions for this category
        public ICollection<ServerVoiceChannelRolePermission>? RolePermissions { get; set; } //all rolepermissions linked to this category
        public ICollection<ServerVoiceChannelMemberSettings>? MemberSettings { get; set; } //member specific definitions for this category
        public ICollection<ServerVoiceChannelMemberPermission>? MemberPermissions { get; set; } //all memberpermissions linked to this category
    }
}
