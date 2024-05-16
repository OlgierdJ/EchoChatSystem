using CoreLib.Interfaces;

namespace CoreLib.Entities.Base
{
    public abstract class BaseMessage<TId> : BaseEntity<TId>, IMessage
    {
        public string Content { get; set; }
        public DateTime TimeSent { get; set; }
    }
    public abstract class BaseMessage<TId, TAuthor, TAuthorId> : BaseMessage<TId>
        where TAuthor : IEntity<TAuthorId>
    {
        public TAuthorId? AuthorId { get; set; } //mayb not nullable since you cant delete account? or was it because system messages has no author? i cant remember cause 2 hr sleep lol
        public TAuthor? Author { get; set; }
    }
    public abstract class BaseMessage<TId, TAuthor, TAuthorId, TMessageHolder, TMessageHolderId> : BaseMessage<TId, TAuthor, TAuthorId>
        where TAuthor : IEntity<TAuthorId>
        where TMessageHolder : IEntity<TMessageHolderId>
    {
        public TMessageHolderId? MessageHolderId { get; set; }
        public TMessageHolder? MessageHolder { get; set; }
    }
    public abstract class BaseMessage<TId, TAuthor, TAuthorId, TMessageHolder, TMessageHolderId, TParentMessage> : BaseMessage<TId, TAuthor, TAuthorId, TMessageHolder, TMessageHolderId>
        where TAuthor : IEntity<TAuthorId>
        where TMessageHolder : IEntity<TMessageHolderId>
    {
        public TId? ParentId { get; set; }

        public TParentMessage? Parent { get; set; }
        public ICollection<TParentMessage>? Children { get; set; }
    }
}
