using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;

namespace CoreLib.Entities.EchoCore.ChatCore
{
    public class ChatParticipancy : BaseParticipancy<Account, ulong, Chat, ulong>
    {
        public bool Hidden { get; set; }
        public bool IsOwner { get; set; }
    }
}
