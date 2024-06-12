using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.ApplicationCore.SubscriptionCore;

namespace CoreLib.Entities.EchoCore.ApplicationCore;

public class AcceptedCurrency : BaseEntity<uint>
{
    public string Name { get; set; }

    public ICollection<Subscription>? Subscriptions { get; set; }
    public ICollection<SubscriptionTransactionGroup>? TransactionGroups { get; set; }
    public ICollection<SubscriptionTransaction>? Transactions { get; set; }
}