using Microsoft.AspNetCore.SignalR;
using DomainCoreApi.Hubs;
using CoreLib;
using CoreLib.Hubs;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Abstractions;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace DomainCoreApi.Services
{
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
                Entity = JsonSerializer.Serialize(evt.Entity, evt.Entity.GetType(), new JsonSerializerOptions()
                {
                    ReferenceHandler = ReferenceHandler.Preserve
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

}
