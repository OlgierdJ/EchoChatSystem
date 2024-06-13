using Echo.Application.Contracts.Interfaces.Contracts;
using Echo.Domain.Shared.Entities.Base;
using Echo.Domain.Shared.Entities.EchoCore.AccountCore;

namespace Echo.Domain.Shared.Entities.EchoCore.FriendCore;

public class IncomingFriendRequest : BaseEntity<ulong>, IIncomingRequest<ulong, Account, ulong, OutgoingFriendRequest>
{
    public ulong SenderRequestId { get; set; }
    public ulong ReceiverId { get; set; }
    public Account Receiver { get; set; }
    public OutgoingFriendRequest SenderRequest { get; set; }
}