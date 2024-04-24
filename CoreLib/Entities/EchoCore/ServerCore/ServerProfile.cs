using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.ServerCore
{
    public class ServerProfile : BaseEntity<ulong>
    {
        public ulong AccountId { get; set; }
        public ulong ServerId { get; set; }
        public Server Server { get; set; }
        public Account Account { get; set; }
    }
}
