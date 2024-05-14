using CoreLib.DTO.EchoCore.AccountCore;
using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ChatCore;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.ServerCore.ChannelCore.TextChannel
{
    public class ServerTextChannelAccountMessageTracker : BaseEntityTracker<Account, ulong, ServerTextChannel, ulong, ServerTextChannelMessage, ulong>
    {
    }
}
