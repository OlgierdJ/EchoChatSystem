﻿using Microsoft.AspNetCore.SignalR;
using DomainCoreApi.Hubs;
using CoreLib;
using CoreLib.Hubs;

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
            await _publisher.Clients.All.ReceiveDomainEvents(DomainEvents);
            DomainEvents.Clear();
        }

        public async Task LoadDomainEventsAsync(ICollection<DomainEvent> incomingDomainEvents)
        {
            //dont need to process since it will be sent in chronological order probably
            DomainEvents.AddRange(incomingDomainEvents);
        }
    }

}
