using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Echo.Domain.EntityFrameworkCore.DomainEvents;
using Echo.Application.Contracts.Interfaces.Contracts;
using Echo.Domain.Shared.Enums;

namespace Echo.Domain.EntityFrameworkCore.EFCORE.Interceptors;


public sealed class PublishDomainEventsInterceptor : SaveChangesInterceptor
{
    private readonly IDomainEventService _domainEventService;
    public PublishDomainEventsInterceptor(IDomainEventService domainEventService)
    {
        _domainEventService = domainEventService;
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
            .Select(entry => new PreDomainEvent()
            {
                Type = entry.Entity.GetType().AssemblyQualifiedName,
                Entry = entry,
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
        if (eventData.Context.Database.CurrentTransaction == null && //publish after savechanges in case of no "global transaction"
            eventData.Context is not null)
        {
            await _domainEventService.PublishDomainEventsAsync();
        }

        return result;
    }
}

