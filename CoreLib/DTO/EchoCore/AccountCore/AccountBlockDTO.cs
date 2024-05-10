using CoreLib.Entities.Base;
using CoreLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.AccountCore
{
    public class AccountBlockDTO //: BaseBlock<ulong, Account, ulong, Account, ulong>
    {
        public ulong BlockedId { get; set; }
        public DateTime TimeBlocked { get; set; }
    }
}
