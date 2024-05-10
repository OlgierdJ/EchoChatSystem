using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.ChatCore
{
    public class ChatAccountMessageTrackerDTO //: BaseEntityTracker<ulong, Account, ulong, ChatMessage, ulong, Chat, ulong>
    {
        public ulong OwnerId { get; set; } //owner entity (cascade)
        public ulong? SubjectId { get; set; } //tracked entity
        public ChatMessageDTO Subject { get; set; }
    }
}
