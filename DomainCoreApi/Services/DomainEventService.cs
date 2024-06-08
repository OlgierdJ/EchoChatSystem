using Microsoft.AspNetCore.SignalR;
using DomainCoreApi.Hubs;
using CoreLib;
using CoreLib.Hubs;
using CoreLib.Entities.EchoCore.AccountCore;

namespace DomainCoreApi.Services
{
    public class DomainEventService
    {
        private readonly IHubContext<DomainPushNotificationHub, IDomainNotificationHub> _publisher;
        private List<DomainEvent> DomainEvents { get; set; } = new();
        public DomainEventService(IHubContext<DomainPushNotificationHub, IDomainNotificationHub> publisher)
        {
            _publisher = publisher;
        }
        public async Task PublishDomainEventsAsync()
        {
            //var testList = new List<DomainEvent>() { new() { Type = "test", Action = CoreLib.Entities.Enums.EntityAction.Unchanged, Entity = new Account() { Id = 29, ActivityStatus = new() { Id = 54, Name = "off" } } } };
            await _publisher.Clients.All.ReceiveDomainEvents(DomainEvents);
            //await _publisher.Clients.All.ReceiveDomainEvents(testList);
            DomainEvents.Clear();
        }

        public async Task LoadDomainEventsAsync(ICollection<DomainEvent> incomingDomainEvents)
        {
            //dont need to process since it will be sent in chronological order probably
            DomainEvents.AddRange(incomingDomainEvents);
        }
    }

}
