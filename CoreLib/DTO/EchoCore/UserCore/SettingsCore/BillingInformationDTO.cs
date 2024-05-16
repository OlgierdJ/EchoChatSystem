using CoreLib.DTO.EchoCore.UserCore.SubscriptionCore;

namespace CoreLib.DTO.EchoCore.UserCore.SettingsCore
{
    public class BillingInformationDTO
    {
        public ulong Id { get; set; }
        public ICollection<PaymentMethodDTO>? PaymentMethods { get; set; }
        public ICollection<SubscriptionTransactionDTO>? Transactions { get; set; }
    }
}
