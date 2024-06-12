using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ApplicationCore.SubscriptionCore;

namespace CoreLib.Entities.EchoCore.ApplicationCore.SettingsCore;

public class BillingInformation : BaseEntity<ulong>
{
    //public ulong AccountSettingsId { get; set; }
    public AccountSettings AccountSettings { get; set; }
    public ICollection<PaymentMethod>? PaymentMethods { get; set; }
    public ICollection<Subscription>? Subscriptions { get; set; }
}
