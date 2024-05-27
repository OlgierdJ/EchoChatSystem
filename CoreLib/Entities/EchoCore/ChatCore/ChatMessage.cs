using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;

namespace CoreLib.Entities.EchoCore.ChatCore
{
    public class ChatMessage : BaseMessage<ulong, Account, ulong, Chat, ulong, ChatMessage, ulong?>
    {
        public ICollection<ChatMessageAttachment>? Attachments { get; set; }
        //public ICollection<ChatMessageReport> Reports { get; set; }
        public ChatMessagePin? MessagePin { get; set; }
        public ICollection<ChatAccountMessageTracker>? MessageTrackers { get; set; }
    }
}
