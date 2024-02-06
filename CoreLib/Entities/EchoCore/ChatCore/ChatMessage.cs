using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.ChatCore
{
    public class ChatMessage : BaseMessage<ulong, Account, ulong, Chat, ulong>
    {
        public ICollection<ChatMessageAttachment> MessageAttachments { get; set; }
    }
}
