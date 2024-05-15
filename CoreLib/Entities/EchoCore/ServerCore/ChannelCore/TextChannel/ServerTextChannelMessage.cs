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
    public class ServerTextChannelMessage : BaseMessage<ulong, Account, ulong, ServerTextChannel, ulong, ServerTextChannelMessage>
    {
        public ICollection<ServerTextChannelMessageAttachment>? Attachments { get; set; }
        public ServerTextChannelMessagePin? MessagePin { get; set; }
        public ICollection<ServerTextChannelAccountMessageTracker>? MessageTrackers { get; set; }
    }
}
