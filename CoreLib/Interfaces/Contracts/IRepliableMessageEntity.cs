namespace CoreLib.Interfaces.Contracts
{
    public interface IRepliableMessageEntity<TParentMessage, TParentMessageId>
    {
        public TParentMessageId ParentId { get; set; }

        public TParentMessage? Parent { get; set; }
        public ICollection<TParentMessage>? Children { get; set; }
    }
}
