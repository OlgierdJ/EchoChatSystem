using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;

namespace CoreLib.Entities.EchoCore.ChatCore
{
    public class ChatAccountMessageTracker : BaseEntityTracker<Account, ulong, Chat, ulong, ChatMessage, ulong?>
    {
    }
}
