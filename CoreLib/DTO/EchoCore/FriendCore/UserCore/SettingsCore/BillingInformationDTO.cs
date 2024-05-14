using CoreLib.DTO.EchoCore.AccountCore;
using CoreLib.DTO.EchoCore.FriendCore.UserCore.SubscriptionCore;
using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ApplicationCore.SubscriptionCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.FriendCore.UserCore.SettingsCore
{
    public class BillingInformationDTO
    {
        public ulong Id { get; set; }
        public ICollection<PaymentMethodDTO>? PaymentMethods { get; set; }
        public ICollection<SubscriptionTransactionDTO>? Transactions { get; set; }
    }
}
