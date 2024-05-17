using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore.Category;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.GeneralCore.RoleCore
{
    public class ServerPermissionConfiguration : BaseEntity<ulong> //reused serverpermission lists all permissions that can be used on the server
    {
        public byte? CategoryId { get; set; } //null = globally displayed permissions
        public string Name { get; set; } //displayed name
        public string? Description { get; set; } //what is does / should do

        public ServerPermissionCategoryConfiguration? Category { get; set; } //used for grouping permissions together
        public ICollection<ServerRolePermissionConfiguration>? RolePermissions { get; set; } //server global permissions
        public ICollection<ServerChannelCategoryMemberSettings>? CategoryMemberSettings { get; set; } //category wide permissiongroups overridden by channelspecific permissions
        public ICollection<ServerChannelCategoryMemberPermission>? CategoryMemberPermissions { get; set; } //category wide permissions overridden by channelspecific permissions
        public ICollection<ServerTextChannelMemberSettings>? TextChannelMemberSettings { get; set; } //channel wide permissiongroups
        public ICollection<ServerTextChannelMemberPermission>? TextChannelMemberPermissions { get; set; }  //channel wide permissions
        public ICollection<ServerVoiceChannelMemberSettings>? VoiceChannelMemberSettings { get; set; } //channel wide permissiongroups
        public ICollection<ServerVoiceChannelMemberPermission>? VoiceChannelMemberPermissions { get; set; } //channel wide permissions
    }
}
