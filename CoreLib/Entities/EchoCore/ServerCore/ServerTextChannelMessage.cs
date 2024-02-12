using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.ServerCore
{
    public class ServerTextChannelMessage : BaseMessage<ulong, Account, ulong, ServerTextChannel, ulong, ServerTextChannelMessage>
    {
    }
}
