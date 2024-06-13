using Echo.Application.Contracts.Interfaces.Contracts;
using Echo.Application.Contracts.Interfaces.Contracts;

namespace Echo.Domain.Shared.Entities.EchoCore.AccountCore;

public class AccountBlock : IBlock<Account, ulong, Account, ulong>, IDomainEntity
{
    //You cease to be friends with them;
    //You are unable to send them a friend request;
    //You are unable to direct message them;
    //You are unable to immediately read their messages(unless you click on the box to view them)
    public ulong BlockerId { get; set; }
    public ulong BlockedId { get; set; }
    public DateTime TimeBlocked { get; set; }
    public Account Blocker { get; set; }
    public Account Blocked { get; set; }
}
