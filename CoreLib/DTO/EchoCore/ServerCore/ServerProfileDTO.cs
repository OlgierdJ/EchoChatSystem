using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.ServerCore
{
    public class ServerProfileDTO //: BaseEntity<ulong>
    {
        public ulong AccountId { get; set; }
        public ulong ServerId { get; set; }
    }
}
