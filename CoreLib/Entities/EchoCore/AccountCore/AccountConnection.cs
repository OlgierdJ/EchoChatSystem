using CoreLib.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.AccountCore
{
    public class AccountConnection : BaseEntity<ulong>
    {
        public ulong AccountId { get; set; }
        public string ExternalProvider { get; set; }
        public string InternalProvider { get; set; }
        public string ExternalToken { get; set; }
        public string InternalToken { get; set; }

        public Account Account { get; set; }
    }
}