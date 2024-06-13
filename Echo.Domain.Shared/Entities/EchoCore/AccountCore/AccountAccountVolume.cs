using Echo.Application.Contracts.Interfaces.Contracts;

namespace Echo.Domain.Shared.Entities.EchoCore.AccountCore;

public class AccountAccountVolume : IDomainEntity
{
    public ulong OwnerId { get; set; }
    public ulong SubjectId { get; set; }
    public byte Volume { get; set; }

    public Account Owner { get; set; }
    public Account Subject { get; set; }
}
