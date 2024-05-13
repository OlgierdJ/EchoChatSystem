using CoreLib.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.ApplicationCore.SubscriptionCore
{
    public class SubscriptionTransaction : BaseEntity<ulong> 
        //the original transactions used to calculate paid amount vs needed amount for each activeperiod needs unique id per transaction
        //cause transactions can be paid partially
    {
        /*
         * maybe find out if each transaction should save platform or the payment method or a duplicate of it?
         * think maybe it should be platform, transactionid
         * 
         */


        public ulong TransactionGroupId { get; set; } //allows for multiple seperate transactions to the same productinstance payment
        public uint CurrencyId { get; set; }
        /// <summary>
        /// Used for tracking the charged amount in case of audits
        /// </summary>
        public double ChargedAmount { get; set; }
        public DateTime TimePaid { get; set; }
        public PaymentType TransactionType { get; set; } //paypal or stripe for knowing where to verify and refund
        public string ExternalTransactionId { get; set; } //paypal or stripe transaction id used for interacting with the external payment system
        /// <summary>
        /// Currency which is charged from payment method
        /// </summary>
        public AcceptedCurrency Currency { get; set; } //currency of the charged amount
        public SubscriptionTransactionGroup TransactionGroup { get; set; } //instance which has been fully or partially paid for.
        public SubscriptionTransactionRefund? Refund { get; set; }
    }
}
