namespace CoreLib.Entities.Base
{
    public interface IOwnedPinboard<TMessagePin, TOwner, TOwnerId> : IPinboard<TMessagePin>
    {
        public TOwnerId OwnerId { get; set; }
        public TOwner Owner { get; set; }
    }
}
