using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.ApplicationCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.AccountCore
{
    public class AccountRoleDTO //: BaseEntity<ulong>
        //thought is that an Account can be both guest, registereduser, applicationsupport, applicationmoderater, applicationadmin, applicationsuperadmin, etc
    {
        public ulong AccountId { get; set; }
        public ulong RoleId { get; set; }
    }
}
