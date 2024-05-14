using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLib.DTO.EchoCore.AccountCore;
using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore.Category;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore.VoiceChannel;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.SettingsCore;
using CoreLib.Entities.Enums;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.ChannelCore
{
    public class ServerVoiceChannelConfiguration : BaseChannel<ulong, ServerChannelCategory, ulong, Server, ulong>
    {
        public uint RegionId { get; set; }
        public uint BitRate { get; set; }
        public VideoQualityMode VideoQuality { get; set; }
        public uint UserLimit { get; set; } //default 0 = unlimited, max99 (maybe some other max cause server stability via signalr)
        public ServerRegion Region { get; set; } //this defines the hub server you connect to on voice join.
        public ServerSettings? ServerSettings { get; set; } //mapped through inactivechannel
        public ICollection<AccountServerVoiceChannelMute>? Muters { get; set; } 
        public ICollection<ServerVoiceInvite>? VoiceInvites { get; set; }
        public ICollection<ServerVoiceChannelPermissionConfiguration>? AllowedPermissions { get; set; } //related permissions used for creating subset of all permissions showed to the user
        public ICollection<ServerVoiceChannelRoleConfiguration>? AllowedRoles { get; set; } //all roles with specific defined permissions for this category
        public ICollection<ServerVoiceChannelRolePermissionConfiguration>? RolePermissions { get; set; } //all rolepermissions linked to this category
        public ICollection<ServerVoiceChannelMemberSettingsConfiguration>? MemberSettings { get; set; } //member specific definitions for this category
        public ICollection<ServerVoiceChannelMemberPermissionConfiguration>? MemberPermissions { get; set; } //all memberpermissions linked to this category
    }
}
