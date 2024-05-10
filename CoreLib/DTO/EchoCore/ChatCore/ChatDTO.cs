using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.ChatCore
{
    public class ChatDTO //: BaseMessageHolder<ulong, ChatMessage, Account, ChatPinboard, ChatInvite, ChatMute>
    {
        public string IconUrl { get; set; }
        public ulong chatId { get; set; }
        //make sure the participants know that there is an ongoing call or video call in the chat dont know how variable? live data? how dafuq
        public ICollection<ChatAccountMessageTrackerDTO>? MessageTrackers { get; set; }
    }
}
