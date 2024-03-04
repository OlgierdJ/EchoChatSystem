using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.ChatCore
{
    public class ChatAccountMessageTracker : BaseEntity<ulong>
    {
        public ulong AccountId { get; set; } //account that the tracker belongs to (cascade)
        public ulong ChatId { get; set; } //chat that the tracker belongs to (cascade)
        public ulong? ChatMessageId { get; set; } //latest viewed message for the specific account
        public Account Account { get; set; }
        public Chat Chat { get; set; }
        public ChatMessage? Message { get; set; }
    }
}
