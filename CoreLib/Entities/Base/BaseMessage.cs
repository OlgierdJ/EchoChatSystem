using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.Base
{
    public abstract class BaseMessage<TAuthorEntity, TAuthorEntityId> : BaseEntity<int>
    {
        public TAuthorEntityId OwnerId { get; set; }
        public string Content { get; set; }
        public DateTime DateAdded { get; set; }

        public TAuthorEntity? Owner{ get; set; }
    }
    public abstract class BaseMessage<TAuthorEntity, TAuthorEntityId, TChatEntity, TChatEntityId> : BaseMessage<TAuthorEntity, TAuthorEntityId>
    {
        public TChatEntityId ChatId { get; set; }
        public TChatEntity? Chat { get; set; }
    }
    public abstract class BaseMessage<TAuthorEntity, TAuthorEntityId, TChatEntity, TChatEntityId, TParentMessageEntity> : BaseMessage<TAuthorEntity, TAuthorEntityId, TChatEntity, TChatEntityId>
    {
        public int? ParentId { get; set; }

        public TParentMessageEntity? Parent { get; set; }
        public IEnumerable<TParentMessageEntity>? Children { get; set; }
    }
}
