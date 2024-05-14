using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.FriendCore.UserCore.SubscriptionCore
{
    public class PaymentMethodDTO
    {
        public ulong Id { get; set; }
        public PaymentTypeDTO Type { get; set; }
        //public BillingAddressDTO Type { get; set; } //perhaps make this entity
        public DateTime TimeAdded { get; set; }
        public bool IsDefaultMethod { get; set; }
        public string Description { get; set; } //paypal email, name on card, etc
    }
}
