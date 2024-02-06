using CoreLib.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore
{
    public class Permission : BaseEntity<int>
    {
        public int RolePermissionId { get; set; }
        public bool CanBanUsers { get; set; }
        public bool CanDeleteUsers { get; set; }
        public bool CanEditMessage { get; set; }
        public bool CanDeleteMessage { get; set; }
        public bool CanSendMessage { get; set; }
        public RolePermission RolePermission { get; set; }
    }
}
