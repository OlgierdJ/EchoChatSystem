using CoreLib.Abstractions;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using CoreLib.Entities.Base;
using CoreLib.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using Microsoft.AspNetCore.SignalR;
using DomainCoreApi.Hubs;
using CoreLib;
using CoreLib.Hubs;
using CoreLib.Entities.Enums;

namespace DomainCoreApi.EFCORE.Interceptors
{
    internal sealed class PublishDomainEventsInterceptor : SaveChangesInterceptor
    {
        private readonly IHubContext<DomainPushNotificationHub, IDomainNotificationHub> _publisher;
        private List<DomainEvent> DomainEvents { get; set; }

        public PublishDomainEventsInterceptor(IHubContext<DomainPushNotificationHub, IDomainNotificationHub> publisher)
        {
            _publisher = publisher;
        }

        //before
        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            if (eventData.Context is not null)
            {
                await LoadDomainEventsAsync(eventData.Context);
            }
            return await base.SavingChangesAsync(eventData, result, cancellationToken); //maybe works
        }

        private async Task LoadDomainEventsAsync(DbContext context)
        {
            DomainEvents = context
                .ChangeTracker
                .Entries<IEntity>()
                //find entries that are added, updated or deleted
                .Where(entry => entry.State != EntityState.Unchanged && entry.State != EntityState.Detached)
                .Select(entry => new DomainEvent()
                {
                    Type = entry.Entity.GetType().Name,
                    Entity = entry.Entity,
                    Action = (EntityAction)Enum.Parse(typeof(EntityAction), entry.State.ToString())
                })
                .ToList();
        }

        //after
        public override async ValueTask<int> SavedChangesAsync(
            SaveChangesCompletedEventData eventData,
            int result,
            CancellationToken cancellationToken = default)
        {
            if (eventData.Context is not null)
            {
                await PublishDomainEventsAsync();
            }

            return result;
        }
        private async Task PublishDomainEventsAsync()
        {
            await _publisher.Clients.All.ReceiveDomainEvents(DomainEvents);
        }
    }
    
}
