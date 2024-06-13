using Echo.Application.Contracts.Interfaces.Contracts;
using Echo.Domain.Shared.Entities.Base;
using Echo.Domain.Shared.Entities.EchoCore.ServerCore.ChannelCore.Category;
using Echo.Domain.Shared.Entities.EchoCore.ServerCore.ChannelCore.TextChannel;
using Echo.Domain.Shared.Entities.EchoCore.ServerCore.ChannelCore.VoiceChannel;
using Echo.Domain.Shared.Entities.EchoCore.ServerCore.GeneralCore;

namespace Echo.Domain.Shared.Entities.EchoCore.ServerCore.GeneralCore.RoleCore;

public class ServerRole : BaseEntity<ulong>, IRole<ServerProfileServerRole, ServerRolePermission>, IOwned<Server, ulong>
{
    public ulong OwnerId { get; set; } //perhaps not needed?
    public int Importance { get; set; } //perhaps not needed?
    public string Colour { get; set; } //perhaps not needed?
    public string IconURL { get; set; } //perhaps not needed?
    public bool DisplaySeperatelyFromOnlineMembers { get; set; } //perhaps not needed?
    public bool AllowAnyoneToMention { get; set; } //perhaps not needed?
    public bool IsAdmin { get; set; } //perhaps not needed?
    public string Name { get; set; }


    public Server Owner { get; set; } //perhaps not needed?

    //these permissions override the global permisssions for this section.
    public ICollection<ServerChannelCategoryRole>? ChannelCategoryRoles { get; set; } //role overrides within channelcategories
    public ICollection<ServerTextChannelRole>? TextChannelRoles { get; set; } //role overrides within textchannels
    public ICollection<ServerVoiceChannelRole>? VoiceChannelRoles { get; set; } //role overrides within voicechannels
    public ICollection<ServerChannelCategoryRolePermission>? ChannelCategoryRolePermissions { get; set; } //permissions allowed within channelcategories, for the role.
    public ICollection<ServerTextChannelRolePermission>? TextChannelRolePermissions { get; set; } //permissions allowed within textchannel, for the role.
    public ICollection<ServerVoiceChannelRolePermission>? VoiceChannelRolePermissions { get; set; } //permissions allowed within voicechannel, for the role.
    public ICollection<ServerProfileServerRole>? Recipients { get; set; }
    public ICollection<ServerRolePermission>? Permissions { get; set; }
}
