namespace Echo.Application.Contracts.Interfaces.Contracts;

public interface IEntity<TId> : IEntity
{
    new TId Id { get; set; }
}

public interface IEntity : IDomainEntity
{
    object Id { get; set; }
}
