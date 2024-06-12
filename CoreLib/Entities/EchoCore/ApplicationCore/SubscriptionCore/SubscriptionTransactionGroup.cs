using CoreLib.Entities.Base;

namespace CoreLib.Entities.EchoCore.ApplicationCore.SubscriptionCore;

public class SubscriptionTransactionGroup : BaseEntity<ulong>
//in case of refunds by policy we've chosen to issue full refunds for all the transactions of the group but they still need to be individually audited.
{
    //used for calculating needed amount to fulfill subscriptionactiveperiods
    public uint CurrencyId { get; set; } //currency type of the amounttobepaid used for conversions if payment is made in other currencies
    public double ToBePaidAmount { get; set; } //used for setting fulfilled flag in case of full payment.
    public bool Fulfilled { get; set; } //flag specifying whether or not the group has been fulfilled overall and therefore should be ignored
    public AcceptedCurrency Currency { get; set; }
    /// <summary>
    /// collection of the transactions which will make up the full amount of the payment.
    /// </summary>
    public ICollection<SubscriptionTransaction>? Transactions { get; set; }
    /// <summary>
    /// Collection of activeperiods cause multiple periods can be paid by the same transactiongroup
    /// in case the subscription has been paused and reactivated into a new subscriptionactiveperiod
    /// </summary>
    public ICollection<SubscriptionActivePeriod>? ActivePeriods { get; set; }
}
