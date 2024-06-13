using Echo.Application.Contracts.Interfaces.Contracts;

namespace Echo.Domain.Shared.Entities.EchoCore.AccountCore;


public class AccountDirectMessageRelation : IDomainEntity
{
    public ulong OwnerId { get; set; }
    public ulong RelationId { get; set; }
    public Account Owner { get; set; }
    public DirectMessageRelation Relation { get; set; }
}
