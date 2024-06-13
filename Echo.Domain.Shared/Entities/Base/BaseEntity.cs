using Echo.Application.Contracts.Interfaces.Contracts;

namespace Echo.Domain.Shared.Entities.Base;

public abstract class BaseEntity<TId> : IEntity<TId> // done
{
    public TId Id { get; set; }
    object IEntity.Id
    {
        get { return Id; }
        set { Id = (TId)value; }
    }
}
