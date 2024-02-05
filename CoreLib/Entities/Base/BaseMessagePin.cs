using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.Base
{
    public abstract class BaseMessagePin<TId, TMessage, TMessageId, TPinboard, TPinboardId> : BaseEntity<TId>
    {
        public TPinboardId PinboardId { get; set; }
        public TMessageId MessageId { get; set; }
        public DateTime TimePinned { get; set; }

        public TPinboard Pinboard { get; set; }
        public TMessage Message { get; set; }
    }
}
