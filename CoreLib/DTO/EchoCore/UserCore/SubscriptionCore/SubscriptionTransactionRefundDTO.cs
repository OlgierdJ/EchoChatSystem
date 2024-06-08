using CoreLib.DTO.Contracts;

namespace CoreLib.DTO.EchoCore.UserCore.SubscriptionCore
{

    public class SubscriptionTransactionRefundDTO : ISubscriptionTransactionRefund
    {
        public ulong Id { get; set; } //using same id as transaction
        public DateTime TimeRefunded { get; set; }
        public string Reason { get; set; }
    }
}
