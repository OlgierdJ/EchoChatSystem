using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Interfaces.Contracts;

namespace CoreLib.Entities.EchoCore.FriendCore;

public class IncomingFriendRequest : BaseEntity<ulong>, IIncomingRequest<ulong, Account, ulong, OutgoingFriendRequest>
{
    public ulong SenderRequestId { get; set; }
    public ulong ReceiverId { get; set; }
    public Account Receiver { get; set; }
    public OutgoingFriendRequest SenderRequest { get; set; }
}