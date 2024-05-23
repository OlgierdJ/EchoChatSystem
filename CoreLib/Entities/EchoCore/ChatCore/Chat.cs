using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;

namespace CoreLib.Entities.EchoCore.ChatCore
{
    public class Chat : BaseMessageHolder<ulong, ChatMessage, ChatParticipancy, ChatPinboard, ChatInvite, ChatMute>
    {
        public string IconUrl { get; set; }
        //make sure the participants know that there is an ongoing call or video call in the chat dont know how variable? live data? how dafuq
        public ICollection<ChatAccountMessageTracker>? MessageTrackers { get; set; }
    }
}
