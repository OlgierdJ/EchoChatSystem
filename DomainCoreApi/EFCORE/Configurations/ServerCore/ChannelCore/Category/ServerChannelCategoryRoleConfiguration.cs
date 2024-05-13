using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.RoleCore;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.ChannelCore.Category
{
    public class ServerChannelCategoryRoleConfiguration
    {
        //combined pk
        //channelcategory owner
        public ulong ChannelCategoryId { get; set; }
        //base role
        public ulong RoleId { get; set; }

        public ServerChannelCategoryConfiguration ChannelCategory { get; set; }
        public ServerRole Role { get; set; }
        //independent permissions from the global permissions in server.
        //(these permissions are weighed more than the global server permissions except for serveradmin)
        public ICollection<ServerChannelCategoryRolePermissionConfiguration>? Permissions { get; set; }
    }
}
