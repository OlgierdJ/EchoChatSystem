using CoreLib.DTO.EchoCore.AccountCore;
using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.ChatCore
{
    public class ChatMessage : BaseMessage<ulong, Account, ulong, Chat, ulong, ChatMessage>
    {
        public ICollection<ChatMessageAttachment>? Attachments { get; set; }
        //public ICollection<ChatMessageReport> Reports { get; set; }
        public ChatMessagePin? MessagePin { get; set; }
        public ICollection<ChatAccountMessageTracker>? MessageTrackers { get; set; }
    }
}
