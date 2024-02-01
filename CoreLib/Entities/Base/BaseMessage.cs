using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.Base
{
    public abstract class BaseMessage<TAuthorEntity, TAuthorEntityId> : BaseEntity<ulong>
    {
        public TAuthorEntityId OwnerId { get; set; }
        public string Content { get; set; }
        public DateTime DateSent { get; set; }

        public TAuthorEntity? Owner{ get; set; }
    }
    public abstract class BaseMessage<TAuthorEntity, TAuthorEntityId, TMessageHolderEntity, TMessageHolderEntityId> : BaseMessage<TAuthorEntity, TAuthorEntityId>
    {
        public TMessageHolderEntityId MessageHolderId { get; set; }
        public TMessageHolderEntity? MessageHolder { get; set; }
    }
    public abstract class BaseMessage<TAuthorEntity, TAuthorEntityId, TMessageHolderEntity, TMessageHolderEntityId, TParentMessageEntity> : BaseMessage<TAuthorEntity, TAuthorEntityId, TMessageHolderEntity, TMessageHolderEntityId>
    {
        public ulong? ParentId { get; set; }

        public TParentMessageEntity? Parent { get; set; }
        public IEnumerable<TParentMessageEntity>? Children { get; set; }
    }
}
