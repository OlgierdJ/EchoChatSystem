using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore.Category;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore.TextChannel;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.ManagementCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.SettingsCore;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.ChannelCore
{
    public class ServerTextChannelConfiguration : BaseChannel<ulong, ServerChannelCategory, ulong, Server, ulong>
    {
        public ServerSettings? ServerSettings { get; set; } //mapped through systemmessageschannel
        public ServerTextChannelPinboard? Pinboard { get; set; }
        public ICollection<ServerWebhook>? Webhooks { get; set; } //webhooks monitoring or posting to this channel
        public ICollection<ServerTextChannelMessage>? Messages { get; set; }
        public ICollection<ServerTextChannelAccountMessageTracker>? MessageTrackers { get; set; }
        public ICollection<ServerInvite>? Invites { get; set; }

        //maybe add notification setting (all, onlymentions, nothing) to the mute or a specific new object with linked collection
        public ICollection<AccountServerTextChannelMute>? Muters { get; set; }
        //allowed groups or specific permission????
        public ICollection<ServerTextChannelPermissionConfiguration>? AllowedPermissions { get; set; } //related permissions used for creating subset of all permissions showed to the user

        public ICollection<ServerTextChannelRoleConfiguration>? AllowedRoles { get; set; } //all roles with specific defined permissions for this category
        public ICollection<ServerTextChannelRolePermissionConfiguration>? RolePermissions { get; set; } //all rolepermissions linked to this category
        public ICollection<ServerTextChannelMemberSettingsConfiguration>? MemberSettings { get; set; } //member specific definitions for this category
        public ICollection<ServerTextChannelMemberPermissionConfiguration>? MemberPermissions { get; set; } //all memberpermissions linked to this category
    }
}
