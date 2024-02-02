using CoreLib.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.AccountCore
{
    public class AccountRole : BaseEntity<ulong>
    {
        public ulong AccountId { get; set; }
        public ulong RoleId { get; set; }
        public Account Account { get; set; }
        public Role Role { get; set; }
    }
}
