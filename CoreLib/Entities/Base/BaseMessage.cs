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
    public abstract class BaseMessage<TAuthorEntity, TAuthorEntityId, TChannelEntity, TChannelEntityId> : BaseMessage<TAuthorEntity, TAuthorEntityId>
    {
        public TChannelEntityId SubjectId { get; set; }
        public TChannelEntity? Subject { get; set; }
    }
    public abstract class BaseMessage<TAuthorEntity, TAuthorEntityId, TChannelEntity, TChannelEntityId, TParentMessageEntity> : BaseMessage<TCommenterEntity, TCommenterEntityId, TSubjectEntity, TSubjectEntityId>
    {
        public int? ParentId { get; set; }

        public TParentMessageEntity? Parent { get; set; }
        public IEnumerable<TParentMessageEntity>? Children { get; set; }
    }
}
