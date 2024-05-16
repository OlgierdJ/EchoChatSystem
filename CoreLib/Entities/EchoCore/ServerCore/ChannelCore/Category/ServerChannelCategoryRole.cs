using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.RoleCore;

namespace CoreLib.Entities.EchoCore.ServerCore.ChannelCore.Category
{
    public class ServerChannelCategoryRole
    {
        //combined pk
        //channelcategory owner
        public ulong ChannelCategoryId { get; set; }
        //base role
        public ulong RoleId { get; set; }

        public ServerChannelCategory ChannelCategory { get; set; }
        public ServerRole Role { get; set; }
        //independent permissions from the global permissions in server.
        //(these permissions are weighed more than the global server permissions except for serveradmin)
        public ICollection<ServerChannelCategoryRolePermission>? Permissions { get; set; }
    }
}
