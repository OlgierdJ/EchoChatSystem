﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.Base
{
    public abstract class BaseMessage<TId> : BaseEntity<TId>
    {
        public string Content { get; set; }
        public DateTime TimeSent { get; set; }
        public bool? IsDeleted { get; set; }
    }
    public abstract class BaseMessage<TId, TAuthor, TAuthorId> : BaseMessage<TId>
    {
        public TAuthorId AuthorId { get; set; }
        public TAuthor? Author { get; set; }
    }
    public abstract class BaseMessage<TId, TAuthor, TAuthorId, TMessageHolder, TMessageHolderId> : BaseMessage<TId, TAuthor, TAuthorId>
    {
        public TMessageHolderId MessageHolderId { get; set; }
        public TMessageHolder? MessageHolder { get; set; }
    }
    public abstract class BaseMessage<TId, TAuthor, TAuthorId, TMessageHolder, TMessageHolderId, TParentMessage> : BaseMessage<TId, TAuthor, TAuthorId, TMessageHolder, TMessageHolderId>
    {
        public TId? ParentId { get; set; }

        public TParentMessage? Parent { get; set; }
        public ICollection<TParentMessage>? Children { get; set; }
    }
}
