using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore.Category;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore.TextChannel;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.ManagementCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.SettingsCore;

namespace CoreLib.Entities.EchoCore.ServerCore.ChannelCore
{
    public class ServerTextChannel : BaseEntity<ulong>, IOwnedChannel<Server, ulong>,
        ICategorisableChannel<ServerChannelCategory, ulong>,
        IPrivatisable,
        IAgeRestrictable, 
        IMessageHolder<ServerTextChannelMessage>, 
        IPinboard<ServerTextChannelMessagePin>
    {
        public int OrderWeight { get; set; }
        public ServerSettings? ServerSettings { get; set; } //mapped through systemmessageschannel
        //public ServerTextChannelPinboard? Pinboard { get; set; }
        public ICollection<ServerWebhook>? Webhooks { get; set; } //webhooks monitoring or posting to this channel
        public ICollection<ServerTextChannelMessage>? Messages { get; set; }
        public ICollection<ServerTextChannelMessagePin>? PinnedMessages { get; set; }
        public ICollection<AccountServerTextChannelMessageTracker>? MessageTrackers { get; set; }
        public ICollection<ServerInvite>? Invites { get; set; }

        //maybe add notification setting (all, onlymentions, nothing) to the mute or a specific new object with linked collection
        public ICollection<AccountServerTextChannelMute>? Muters { get; set; }
        //allowed groups or specific permission????
        public ICollection<ServerTextChannelPermission>? AllowedPermissions { get; set; } //related permissions used for creating subset of all permissions showed to the user

        public ICollection<ServerTextChannelRole>? RoleSettings { get; set; } //all roles with specific defined permissions for this category
        public ICollection<ServerTextChannelRolePermission>? RolePermissions { get; set; } //all rolepermissions linked to this category
        public ICollection<ServerTextChannelMemberSettings>? MemberSettings { get; set; } //member specific definitions for this category
        public ICollection<ServerTextChannelMemberPermission>? MemberPermissions { get; set; } //all memberpermissions linked to this category
        public ulong CategoryId { get; set; }
        public ServerChannelCategory? Category { get; set; }
        public ulong OwnerId { get; set; }
        public Server? Owner { get; set; }
        public string Name { get; set; }
        public string? Topic { get; set; }
        public int SlowMode { get; set; }
        public bool IsAgeRestricted { get; set; }
        public bool IsPrivate { get; set; }
    }
}
