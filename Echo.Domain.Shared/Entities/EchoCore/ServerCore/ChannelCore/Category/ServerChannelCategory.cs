using Echo.Domain.Shared.Entities.Base;
using Echo.Domain.Shared.Entities.EchoCore.ServerCore.ChannelCore.TextChannel;
using Echo.Domain.Shared.Entities.EchoCore.ServerCore.ChannelCore.VoiceChannel;
using Echo.Domain.Shared.Entities.EchoCore.ServerCore.GeneralCore;

namespace Echo.Domain.Shared.Entities.EchoCore.ServerCore.ChannelCore.Category;

public class ServerChannelCategory : BaseEntity<ulong>
{
    public ulong ServerId { get; set; }
    public int Importance { get; set; } //used for ordering in program
    public string Name { get; set; } //Names can be duplicate but categories will be sorted based on ids
    //public DateTime DateCreated { get; set; } //idk nice to have maybe redundant if we make an audit log
    public bool IsPrivate { get; set; } //privatises the category and all channels within it

    //allowed groups or specific permission????
    public Server Server { get; set; }
    public ICollection<ServerChannelCategoryPermission>? AllowedPermissions { get; set; } //related permissions used for creating subset of all permissions showed to the user

    public ICollection<ServerChannelCategoryRole>? AllowedRoles { get; set; } //all roles with specific defined permissions for this category
    public ICollection<ServerChannelCategoryRolePermission>? RolePermissions { get; set; } //all rolepermissions linked to this category
    public ICollection<ServerChannelCategoryMemberSettings>? MemberSettings { get; set; } //member specific definitions for this category
    public ICollection<ServerChannelCategoryMemberPermission>? MemberPermissions { get; set; } //all memberpermissions linked to this category
    //advanced permission settings per role which are enforced upon all channels in the category
    public ICollection<ServerTextChannel>? TextChannels { get; set; } //textchannels grouped into this category
    public ICollection<ServerVoiceChannel>? VoiceChannels { get; set; } //voicechannels grouped into this category
}