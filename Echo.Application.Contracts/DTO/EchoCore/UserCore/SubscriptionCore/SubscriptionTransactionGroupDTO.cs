using Echo.Application.Contracts.DTO.EchoCore.MiscCore;

namespace Echo.Application.Contracts.DTO.EchoCore.UserCore.SubscriptionCore;

public class SubscriptionTransactionGroupDTO
//in case of refunds by policy we've chosen to issue full refunds for all the transactions of the group but they still need to be individually audited.
{
    //used for calculating needed amount to fulfill subscriptionactiveperiods
    public ulong Id { get; set; }
    public double ToBePaidAmount { get; set; } //used for setting fulfilled flag in case of full payment.
    public bool Fulfilled { get; set; } //flag specifying whether or not the group has been fulfilled overall and therefore should be ignored
    public CurrencyDTO Currency { get; set; }
    /// <summary>
    /// collection of the transactions which will make up the full amount of the payment.
    /// </summary>
    public ICollection<SubscriptionTransactionDTO>? Transactions { get; set; }
}
