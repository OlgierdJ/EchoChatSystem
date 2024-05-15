using CoreLib.DTO.EchoCore.AccountCore;
using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;

namespace CoreLib.Entities.EchoCore.FriendCore
{
    public class IncomingFriendRequest : BaseIncomingRequest<ulong, Account, ulong, OutgoingFriendRequest>
    {
    }
   
}