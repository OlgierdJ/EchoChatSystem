using CoreLib.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.FriendCore.UserCore.SubscriptionCore
{
    public class SubscriptionTransactionRefundDTO 
    {
        public ulong Id { get; set; } //using same id as transaction
        public DateTime TimeRefunded { get; set; }
        public string Reason { get; set; }
    }
}
