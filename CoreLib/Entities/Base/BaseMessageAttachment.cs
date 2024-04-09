using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.Base
{
    public abstract class BaseMessageAttachment<TId, TMessage, TMessageId> : BaseEntity<TId>
    {
        public TMessageId MessageId { get; set; }
        //public string AttachmentType { get; set; }
        public string FileURL { get; set; }

        public TMessage Message { get; set; }
    }
}
