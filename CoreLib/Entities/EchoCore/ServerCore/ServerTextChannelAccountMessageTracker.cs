using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ChatCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.ServerCore
{
    public class ServerTextChannelAccountMessageTracker : BaseEntityTracker<Account, ulong, ServerTextChannel, ulong, ServerTextChannelMessage, ulong>
    {
    }
}
