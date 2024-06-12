using CoreLib;
using CoreLib.Hubs;
using DomainCoreApi.EFCORE.Interceptors;
using DomainCoreApi.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DomainCoreApi.Services;

public class DomainEventService
{
    private readonly IHubContext<DomainPushNotificationHub, IDomainNotificationHub> _publisher;
    private List<PreDomainEvent> DomainEvents { get; set; } = new();
    public DomainEventService(IHubContext<DomainPushNotificationHub, IDomainNotificationHub> publisher)
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
        await _publisher.Clients.All.ReceiveDomainEvents(domainEvts);
        //await _publisher.Clients.All.ReceiveDomainEvents(testList);
        DomainEvents.Clear();
    }

    public async Task LoadDomainEventsAsync(ICollection<PreDomainEvent> incomingDomainEvents)
    {
        //dont need to process since it will be sent in chronological order probably
        DomainEvents.AddRange(incomingDomainEvents);
    }
}
