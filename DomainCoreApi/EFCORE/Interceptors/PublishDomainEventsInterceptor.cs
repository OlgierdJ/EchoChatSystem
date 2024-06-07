using CoreLib.Abstractions;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using CoreLib.Entities.Base;
using CoreLib.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using CoreLib.Hubs;
using CoreLib.Entities.Enums;
using DomainCoreApi.Services;

namespace DomainCoreApi.EFCORE.Interceptors
{

    internal sealed class PublishDomainEventsInterceptor : SaveChangesInterceptor
    {
        private readonly DomainEventService _domainEventService;
        public PublishDomainEventsInterceptor(DomainEventService domainEventService)
        {
            this._domainEventService = domainEventService;
        }

        //before
        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            if (eventData.Context is not null)
            {
                await LoadDomainEventsAsync(eventData.Context);
            }
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private async Task LoadDomainEventsAsync(DbContext context)
        {
            var domainEvents = context
                .ChangeTracker
                .Entries<IDomainEntity>()
                //find entries that are added, updated or deleted
                .Where(entry => entry.State != EntityState.Unchanged && entry.State != EntityState.Detached)
                .Select(entry => new DomainEvent()
                {
                    Type = entry.Entity.GetType().Name,
                    Entity = entry.Entity,
                    Action = (EntityAction)Enum.Parse(typeof(EntityAction), entry.State.ToString())
                })
                .ToList();
            await _domainEventService.LoadDomainEventsAsync(domainEvents);
        }

        //after
        public override async ValueTask<int> SavedChangesAsync(
            SaveChangesCompletedEventData eventData,
            int result,
            CancellationToken cancellationToken = default)
        {
            if (eventData.Context.Database.CurrentTransaction==null && //publish after savechanges in case of no "global transaction"
                eventData.Context is not null)
            {
                await _domainEventService.PublishDomainEventsAsync();
            }

            return result;
        }
        
    }
    
}
