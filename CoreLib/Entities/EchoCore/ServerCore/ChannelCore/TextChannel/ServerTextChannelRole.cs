using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.RoleCore;

namespace CoreLib.Entities.EchoCore.ServerCore.ChannelCore
{
    public class ServerTextChannelRole
    {
        //channelcategory owner
        public ulong ChannelCategoryId { get; set; }
        //base role
        public ulong RoleId { get; set; }

        public ServerTextChannel Channel { get; set; }
        public ServerRole Role { get; set; }
        //independent permissions from the global permissions in server.
        //(these permissions are weighed more than the global server permissions except for serveradmin)
        public ICollection<ServerTextChannelRolePermission>? Permissions { get; set; }
    }
}
