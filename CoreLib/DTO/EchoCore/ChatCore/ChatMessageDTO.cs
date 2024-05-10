using CoreLib.DTO.EchoCore.AccountCore;
using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;

namespace CoreLib.DTO.EchoCore.ChatCore
{
    public class ChatMessageDTO //: BaseMessage<ulong, Account, ulong, Chat, ulong, ChatMessage>
    {
        public ulong ChatId { get; set; }
        public string Content { get; set; }
        public DateTime TimeSent { get; set; }
        public ulong AuthorId { get; set; }

        public ICollection<ChatMessageAttachmentDTO>? Attachments { get; set; }
        //tænker ik vi lave den.
        //public ChatMessagePinDTO? MessagePin { get; set; }
        public ICollection<ChatAccountMessageTrackerDTO>? MessageTrackers { get; set; }
    }
}
