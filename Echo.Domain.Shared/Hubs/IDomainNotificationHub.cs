using Echo.Domain.Shared.DomainEvents;

namespace Echo.Domain.Shared.Hubs;

public interface IDomainNotificationHub
{
    //simple offload processing to other consumers / apis
    Task ReceiveDomainEvents(List<DomainEvent> entity);
}
