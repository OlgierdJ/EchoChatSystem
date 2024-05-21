namespace CoreLib.DTO.EchoCore.UserCore.SubscriptionCore
{
    public interface ISubscriptionTransactionRefund
    {
        ulong Id { get; set; }
        string Reason { get; set; }
        DateTime TimeRefunded { get; set; }
    }

    public class SubscriptionTransactionRefundDTO : ISubscriptionTransactionRefund
    {
        public ulong Id { get; set; } //using same id as transaction
        public DateTime TimeRefunded { get; set; }
        public string Reason { get; set; }
    }
}
