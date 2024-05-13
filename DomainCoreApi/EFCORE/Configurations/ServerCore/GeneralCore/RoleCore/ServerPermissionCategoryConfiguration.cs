using CoreLib.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.GeneralCore.RoleCore
{
    public class ServerPermissionCategoryConfiguration : BaseEntity<byte>
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        public ICollection<ServerPermissionConfiguration>? Permissions { get; set; }
    }
}
