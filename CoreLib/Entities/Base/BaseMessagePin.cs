using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.Base
{
    public abstract class BaseMessagePin<TPinner, TPinnerId, TMessageHolder, TMessageHolderId, TMessage, TMessageId> : BaseEntity<byte>
    {
        public TPinnerId PinnerId { get; set; }
        public TMessageHolderId MessageHolderId { get; set; }
        public TMessageId MessageId { get; set; }
        public DateTime TimePinned { get; set; }

        public TPinner Pinner { get; set; }
        public TMessageHolder MessageHolder { get; set; }
        public TMessage Message { get; set; }
    }
}
