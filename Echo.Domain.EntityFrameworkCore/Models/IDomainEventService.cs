namespace Echo.Domain.EntityFrameworkCore.DomainEvents;
public interface IDomainEventService
{
    Task LoadDomainEventsAsync(ICollection<PreDomainEvent> incomingDomainEvents);
    Task PublishDomainEventsAsync();
}