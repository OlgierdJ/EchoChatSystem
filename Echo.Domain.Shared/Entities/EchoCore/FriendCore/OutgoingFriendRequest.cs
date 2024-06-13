using Echo.Application.Contracts.Interfaces.Contracts;
using Echo.Domain.Shared.Entities.Base;
using Echo.Domain.Shared.Entities.EchoCore.AccountCore;

namespace Echo.Domain.Shared.Entities.EchoCore.FriendCore;

public class OutgoingFriendRequest : BaseEntity<ulong>, IOutgoingRequest<Account, ulong, IncomingFriendRequest>
{
    public ulong SenderId { get; set; }
    public Account Sender { get; set; }
    public IncomingFriendRequest ReceiverRequest { get; set; }
}