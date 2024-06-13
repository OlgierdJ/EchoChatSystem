using Echo.Application.Contracts.Interfaces.Contracts;

namespace Echo.Domain.Shared.Entities.EchoCore.AccountCore;

public class AccountNickname : ITargetedNickname<Account, ulong, Account, ulong>, IDomainEntity
{
    public ulong AuthorId { get; set; }
    public ulong SubjectId { get; set; }
    public string Nickname { get; set; }
    public Account Author { get; set; }
    public Account Subject { get; set; }
}
