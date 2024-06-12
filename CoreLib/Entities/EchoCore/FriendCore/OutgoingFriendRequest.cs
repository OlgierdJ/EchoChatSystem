using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Interfaces.Contracts;

namespace CoreLib.Entities.EchoCore.FriendCore;

public class OutgoingFriendRequest : BaseEntity<ulong>, IOutgoingRequest<Account, ulong, IncomingFriendRequest>
{
    public ulong SenderId { get; set; }
    public Account Sender { get; set; }
    public IncomingFriendRequest ReceiverRequest { get; set; }
}