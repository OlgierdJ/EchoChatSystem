using CoreLib.Entities.Base;
using CoreLib.Interfaces;

namespace CoreLib.Entities.EchoCore.ChatCore
{
    public class ChatMessagePin : IMessagePin<ChatMessage, ulong, Chat, ulong>, IDomainEntity
    {
        public ulong PinboardId { get; set; }
        public ulong MessageId { get; set; }
        public Chat Pinboard { get; set; }
        public ChatMessage Message { get; set; }
    }
}