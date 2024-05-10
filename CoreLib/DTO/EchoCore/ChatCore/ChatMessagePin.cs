using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;

namespace CoreLib.DTO.EchoCore.ChatCore
{
    public class ChatMessagePinDTO //: BaseMessagePin<ulong, ChatMessage, ulong, ChatPinboard, ulong>
    {
        public int ChatPinboardId { get; set; }
    }
}