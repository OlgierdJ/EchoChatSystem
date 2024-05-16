namespace CoreLib.Entities.Base
{
    public abstract class BasePinboard<TId, TOwner, TOwnerId, TMessagePin> : BaseEntity<TId>
    {
        public TOwnerId OwnerId { get; set; }
        public TOwner Owner { get; set; }

        public ICollection<TMessagePin>? PinnedMessages { get; set; }
    }
}
