using Echo.Application.Contracts.Interfaces.Contracts;
using Echo.Domain.Shared.Entities.EchoCore.ServerCore.GeneralCore;

namespace Echo.Domain.Shared.Entities.EchoCore.AccountCore;

public class AccountServerMute : ITargetedMute<Account, ulong, Server, ulong>, IDomainEntity
{
    public ulong SubjectId { get; set; }
    public ulong MuterId { get; set; }
    public DateTime TimeMuted { get; set; }
    public DateTime? ExpirationTime { get; set; }
    public Account Muter { get; set; }
    public Server Subject { get; set; }
}
