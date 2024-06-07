using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.RoleCore;
using CoreLib.Interfaces.Contracts;

namespace CoreLib.Entities.EchoCore.ServerCore.ChannelCore.Category
{
    public class ServerChannelCategoryPermission : IDomainEntity
    //used for mapping displayed permissions within a channelcategory
    {
        //pk is combination of category and permission
        public ulong ChannelCategoryId { get; set; }
        public ulong PermissionId { get; set; }
        public ServerChannelCategory ChannelCategory { get; set; } //cascade
        public ServerPermission Permission { get; set; } //cascade
    }
}
