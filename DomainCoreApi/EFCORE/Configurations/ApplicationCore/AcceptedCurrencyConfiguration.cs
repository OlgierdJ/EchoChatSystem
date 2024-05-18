using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.ApplicationCore.SubscriptionCore;

namespace CoreLib.Entities.EchoCore.ApplicationCore
{
    public class AcceptedCurrencyConfiguration : BaseEntity<uint> //needs default currencies ("dkk"), ("eur"), ("usd")
    {
        public string Name { get; set; }

        public ICollection<Subscription>? Subscriptions { get; set; }
        public ICollection<SubscriptionTransactionGroup>? TransactionGroups { get; set; }
        public ICollection<SubscriptionTransaction>? Transactions { get; set; }
    }
}