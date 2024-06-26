﻿using CoreLib.DTO.EchoCore.UserCore.SubscriptionCore;

namespace CoreLib.DTO.EchoCore.UserCore.SettingsCore
{
    public interface IBillingInformation
    {
        ulong Id { get; set; }
        ICollection<PaymentMethodDTO>? PaymentMethods { get; set; }
        ICollection<SubscriptionTransactionDTO>? Transactions { get; set; }
    }

    public class BillingInformationDTO : IBillingInformation
    {
        public ulong Id { get; set; }
        public ICollection<PaymentMethodDTO>? PaymentMethods { get; set; }
        public ICollection<SubscriptionTransactionDTO>? Transactions { get; set; }
    }
}
