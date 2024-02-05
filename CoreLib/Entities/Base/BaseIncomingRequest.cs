using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.Base
{
    public abstract class BaseIncomingRequest<TId, TReceiver, TReceiverId, TSenderRequest> : BaseEntity<TId>
    {
        public TReceiverId ReceiverId { get; set; }

        public TReceiver Receiver { get; set; }
        public TSenderRequest SenderRequest { get; set; }
    }
}
