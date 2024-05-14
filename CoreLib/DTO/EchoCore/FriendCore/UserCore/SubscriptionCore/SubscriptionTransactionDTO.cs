using CoreLib.DTO.EchoCore.FriendCore.UserCore.MiscCore;
using CoreLib.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.FriendCore.UserCore.SubscriptionCore
{
    public class SubscriptionTransactionDTO
    {
        public ulong Id { get; set; }
        public string Description { get; set; }//essentially subscription type.
        public DateTime TimePaid { get; set; }
        //public ulong TransactionGroupId { get; set; } //allows for multiple seperate transactions to the same productinstance payment
        public PaymentTypeDTO TransactionType { get; set; } //paypal or stripe for knowing where to verify and refund
        public string ExternalTransactionId { get; set; } //paypal or stripe transaction id used for interacting with the external payment system
        public double ChargedAmount { get; set; }
        public CurrencyDTO Currency { get; set; } //currency of the charged amount  
    }
}
