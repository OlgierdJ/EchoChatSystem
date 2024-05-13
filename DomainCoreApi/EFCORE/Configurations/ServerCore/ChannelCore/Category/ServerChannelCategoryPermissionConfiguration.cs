using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.RoleCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.ChannelCore.Category
{
    public class ServerChannelCategoryPermissionConfiguration
        //used for mapping displayed permissions within a channelcategory
    {
        //pk is combination of category and permission
        public ulong ChannelCategoryId { get; set; }
        public ulong PermissionId { get; set; }
        public ServerChannelCategoryConfiguration ChannelCategory { get; set; } //cascade
        public ServerPermission Permission { get; set; } //cascade
    }
}
