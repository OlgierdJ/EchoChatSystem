using DomainCoreApi.Services;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;

namespace DomainCoreApi.EFCORE.Interceptors
{
    internal sealed class PublishTransactionDomainEventsInterceptor : DbTransactionInterceptor
    {
        private readonly DomainEventService _domainEventService;
        public PublishTransactionDomainEventsInterceptor(DomainEventService domainEventService)
        {
            this._domainEventService = domainEventService;
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
    
}
