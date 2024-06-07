using CoreLib.Entities.Base;

namespace CoreLib.Entities.EchoCore.ChatCore
{
    public class ChatMessageAttachment : BaseEntity<ulong>, IMessageAttachment<ChatMessage, ulong>
    {
        public ulong MessageId { get; set; }
        public string FileLocationURL { get; set; }
        public string FileName { get; set; }
        public string? Description { get; set; }
        public ChatMessage Message { get; set; }
    }
}
