using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.Chat
{
    public class Chat : BaseMessageHolder<ChatParticipant, ChatMessage, ChatMessagePin, ChatInvite, ChatMute>
    {
        //make sure the participants know that there is an ongoing call or video call in the chat dont know how variable? live data?
    }
}
