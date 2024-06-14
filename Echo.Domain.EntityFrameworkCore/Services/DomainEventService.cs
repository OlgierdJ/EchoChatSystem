using Echo.Domain.EntityFrameworkCore.DomainEvents;
using Echo.Domain.Shared.DomainEvents;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Echo.Domain.EntityFrameworkCore.Services;

public interface IPublisher
{
    public Task PublishAsync(List<DomainEvent> domainEvents);
}

public class DomainEventService : IDomainEventService
{
    private readonly IPublisher _publisher;
    private List<PreDomainEvent> DomainEvents { get; set; } = new();
    public DomainEventService(IPublisher publisher)
    {
        _publisher = publisher;
    }
    public async Task PublishDomainEventsAsync()
    {
        var domainEvts = DomainEvents.Select(evt => new DomainEvent()
        {
            Entity = JsonSerializer.Serialize(evt.Entry.Entity, evt.Entry.Entity.GetType(), new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                MaxDepth = 128
            }),
            Action = evt.Action,
            Type = evt.Type,
        }).ToList();
        await _publisher.PublishAsync(domainEvts);
        //await _publisher.Clients.All.ReceiveDomainEvents(testList);
        DomainEvents.Clear();
    }

    public async Task LoadDomainEventsAsync(ICollection<PreDomainEvent> incomingDomainEvents)
    {
        //dont need to process since it will be sent in chronological order probably
        DomainEvents.AddRange(incomingDomainEvents);
    }
}
