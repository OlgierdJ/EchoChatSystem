using Echo.Application.Contracts.DTO.Contracts;
using Echo.Application.Contracts.DTO.EchoCore.MiscCore;

namespace Echo.Application.Contracts.DTO.EchoCore.UserCore.SubscriptionCore;


public class SubscriptionTransactionDTO : ISubscriptionTransaction
{
    public ulong Id { get; set; }
    //public string Description { get; set; } //essentially subscription type.
    public DateTime TimePaid { get; set; }
    //public ulong TransactionGroupId { get; set; } //allows for multiple seperate transactions to the same productinstance payment
    public PaymentTypeDTO TransactionType { get; set; } //paypal or stripe for knowing where to verify and refund
    public string ExternalTransactionId { get; set; } //paypal or stripe transaction id used for interacting with the external payment system
    public double ChargedAmount { get; set; }
    public CurrencyDTO Currency { get; set; } //currency of the charged amount  
}
