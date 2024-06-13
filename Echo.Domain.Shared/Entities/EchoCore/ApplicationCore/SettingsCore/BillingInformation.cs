using Echo.Domain.Shared.Entities.Base;
using Echo.Domain.Shared.Entities.EchoCore;
using Echo.Domain.Shared.Entities.EchoCore.AccountCore;
using Echo.Domain.Shared.Entities.EchoCore.ApplicationCore.SubscriptionCore;

namespace Echo.Domain.Shared.Entities.EchoCore.ApplicationCore.SettingsCore;

public class BillingInformation : BaseEntity<ulong>
{
    //public ulong AccountSettingsId { get; set; }
    public AccountSettings AccountSettings { get; set; }
    public ICollection<PaymentMethod>? PaymentMethods { get; set; }
    public ICollection<Subscription>? Subscriptions { get; set; }
}
