using CoreLib.Entities.Base;
using CoreLib.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.ApplicationCore.SubscriptionCore
{
    public class SubscriptionActivePeriod : BaseEntity<ulong>
    {
        public ulong SubcriptionId { get; set; }
        public ulong TransactionGroupId { get; set; }


        /// <summary>
        /// Used for changing the subscription status, like pausing it or suspending it due to breaking ToS.
        /// Paused, Active, Suspended, Refunded
        /// </summary>
        public SubscriptionStatus Status { get; set; }
        /// <summary>
        /// Date of when the subscription starts
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// Time of when the subscription ends
        /// </summary>
        public DateTime ExpirationTime { get; set; }
        /// <summary>
        /// time the subscription was paused used for calculating remaining subscription upon starting it again (effectively creates a new subscription such that only lasts the remaining time such that the start date and end date is different and can be "paused again")
        /// </summary>
        public DateTime? TimePaused { get; set; }


        /// <summary>
        /// Subscription which is related to the transaction
        /// </summary>
        public Subscription Subscription { get; set; }
        public SubscriptionTransactionGroup TransactionGroup { get; set; }
        
    }
}
