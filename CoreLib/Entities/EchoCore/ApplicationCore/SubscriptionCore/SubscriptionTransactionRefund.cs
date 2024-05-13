using CoreLib.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.ApplicationCore.SubscriptionCore
{
    public class SubscriptionTransactionRefund : BaseEntity<ulong>
    {
        //public ulong TransactionId { get; set; } //using same id as transaction
        public DateTime TimeRefunded { get; set; }
        public string Reason { get; set; }
        public SubscriptionTransaction Transaction { get; set; }
    }
}
