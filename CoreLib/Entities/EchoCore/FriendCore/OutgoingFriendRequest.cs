using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;

namespace CoreLib.Entities.EchoCore.FriendCore
{
    public class OutgoingFriendRequest : BaseOutgoingRequest<ulong, Account, ulong, IncomingFriendRequest>
    {
    }
}