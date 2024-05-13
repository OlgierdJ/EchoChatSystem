using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.RoleCore;
using CoreLib.Entities.Enums;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.ChannelCore.Category
{
    public class ServerChannelCategoryRolePermissionConfiguration
    //Used for displaying state of serverpermission for a specific channelcategory for a specific role
    {
        //combine foreign key category and role.
        //pk is combination of category, role and permission
        public ulong ChannelCategoryId { get; set; }
        public ulong RoleId { get; set; }
        public ulong PermissionId { get; set; }
        public bool? State { get; set; } //true = enabled, null = default, false = disabled
        public ServerChannelCategoryRoleConfiguration ChannelCategoryRole { get; set; } //cascade
        public ServerChannelCategoryConfiguration ChannelCategory { get; set; } //ignore
        public ServerRole Role { get; set; } //ignore
        public ServerPermission Permission { get; set; } //cascade
    }
}