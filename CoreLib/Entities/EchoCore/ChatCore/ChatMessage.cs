using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Interfaces;

namespace CoreLib.Entities.EchoCore.ChatCore
{
    public class ChatMessage : BaseEntity<ulong>, IAuditableMessage, IAuthoredEntity<Account, ulong?>, IRepliableMessageEntity<ChatMessage, ulong?>
    {
        public ICollection<ChatMessageAttachment>? Attachments { get; set; }
        //public ICollection<ChatMessageReport> Reports { get; set; }
        public ChatMessagePin? MessagePin { get; set; }
        public ICollection<ChatAccountMessageTracker>? MessageTrackers { get; set; }
        public ulong? ParentId { get; set; }
        public ChatMessage? Parent { get; set; }
        public ICollection<ChatMessage>? Children { get; set; }
        public ulong MessageHolderId { get; set; }
        public Chat MessageHolder { get; set; }
        public ulong? AuthorId { get; set; }
        public Account? Author { get; set; }
        public string Content { get; set; }
        public DateTime TimeSent { get; set; }
        public DateTime? TimeEdited { get; set; }
    }
}
