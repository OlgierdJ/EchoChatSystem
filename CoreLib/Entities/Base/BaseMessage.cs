using CoreLib.Interfaces;

namespace CoreLib.Entities.Base
{
    public  interface IBaseMessage : IMessage
    {
        public string Content { get; set; }
        public DateTime TimeSent { get; set; }
        public DateTime? TimeEdited { get; set; }
    }
    public interface IAuthoredEntity<TAuthor, TAuthorId> 
    {
        public TAuthorId? AuthorId { get; set; } //mayb not nullable since you cant delete account? or was it because system messages has no author? i cant remember cause 2 hr sleep lol
        public TAuthor? Author { get; set; }
    }
    public interface IHeldMessageEntity<TMessageHolder, TMessageHolderId>
    {
        public TMessageHolderId? MessageHolderId { get; set; }
        public TMessageHolder? MessageHolder { get; set; }
    }
    public interface IRepliableMessageEntity<TParentMessage, TParentMessageId>
    {
        public TParentMessageId ParentId { get; set; }

        public TParentMessage? Parent { get; set; }
        public ICollection<TParentMessage>? Children { get; set; }
    }
}
