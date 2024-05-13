using CoreLib.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.ServerCore.GeneralCore.RoleCore
{
    public class ServerPermissionCategory : BaseEntity<byte>
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        public ICollection<ServerPermission>? Permissions { get; set; }
    }
}
