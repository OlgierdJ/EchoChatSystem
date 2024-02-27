using CoreLib.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.AccountCore
{
    public class AccountRole : BaseEntity<ulong> //thought is that an Account can be either guest, registereduser, applicationsupport, applicationmoderater, applicationadmin, applicationsuperadmin, etc
    {
        public ulong AccountId { get; set; }
        public ulong RoleId { get; set; }
        public Account Account { get; set; }
        public Role Role { get; set; }
    }
}
