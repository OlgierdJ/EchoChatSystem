using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.FriendCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.Base
{
    public class BaseOutgoingRequest<TId, TSender, TSenderId, TReceiverRequest> : BaseEntity<TId>
    {
        public TSenderId SenderId { get; set; }

        public TSender Sender { get; set; }
        public TReceiverRequest ReceiverRequest { get; set; }
    }
}
