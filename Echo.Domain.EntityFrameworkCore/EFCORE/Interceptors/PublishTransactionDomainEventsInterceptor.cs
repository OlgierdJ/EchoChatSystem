using Echo.Domain.EntityFrameworkCore.DomainEvents;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;

namespace Echo.Domain.EntityFrameworkCore.EFCORE.Interceptors;

public sealed class PublishTransactionDomainEventsInterceptor : DbTransactionInterceptor
{
    private readonly IDomainEventService _domainEventService;
    public PublishTransactionDomainEventsInterceptor(IDomainEventService domainEventService)
    {
        _domainEventService = domainEventService;
    }
    //public override ValueTask<InterceptionResult> TransactionCommittingAsync(DbTransaction transaction, TransactionEventData eventData, InterceptionResult result, CancellationToken cancellationToken = default)
    //{
    //    return base.TransactionCommittingAsync(transaction, eventData, result, cancellationToken);
    //}

    public override async Task TransactionCommittedAsync(DbTransaction transaction, TransactionEndEventData eventData, CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null) //this will get run if session was global transaction
        {
            await _domainEventService.PublishDomainEventsAsync();
        }
        await base.TransactionCommittedAsync(transaction, eventData, cancellationToken);
    }
}

