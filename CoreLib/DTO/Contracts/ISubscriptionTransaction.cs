using CoreLib.DTO.EchoCore.MiscCore;
using CoreLib.DTO.EchoCore.UserCore.SubscriptionCore;

namespace CoreLib.DTO.Contracts;

public interface ISubscriptionTransaction
{
    double ChargedAmount { get; set; }
    CurrencyDTO Currency { get; set; }
    //string Description { get; set; } //dont think this is needed is it?????
    string ExternalTransactionId { get; set; }
    //ulong Id { get; set; } inherit from iidentified or ientity instead
    DateTime TimePaid { get; set; }
    PaymentTypeDTO TransactionType { get; set; }
}
