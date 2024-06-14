using DomainCoreApi.Hubs;
using Echo.Domain.EntityFrameworkCore.Services;
using Echo.Domain.Shared.DomainEvents;
using Echo.Domain.Shared.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Echo.Chat.API.Services;

public class PushNotificationPublisher : IPublisher
{
    private readonly IHubContext<DomainPushNotificationHub, IDomainNotificationHub> hubContext;

    public PushNotificationPublisher(IHubContext<DomainPushNotificationHub, IDomainNotificationHub> hubContext)
    {
        this.hubContext = hubContext;
    }

    public async Task PublishAsync(List<DomainEvent> domainEvents)
    {
        await hubContext.Clients.All.ReceiveDomainEvents(domainEvents);
    }
}

