using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;

namespace CoreLib.Entities.EchoCore.FriendCore
{
    public class IncomingFriendRequest : BaseEntity<ulong>
    {
        public string ReceiverId { get; set; }

        public Account Receiver { get; set; }
        public OutgoingFriendRequest SenderRequest { get; set; }
    }
}