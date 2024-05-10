using CoreLib.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.ApplicationCore
{
    public class RolePermissionDTO //: BaseEntity<ulong>
    {
        public ulong RoleId { get; set; }
        public ulong PermissionId { get; set; }
        public RoleDTO Role { get; set; }
        public PermissionDTO Permission { get; set; }
    }
}
