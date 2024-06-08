namespace CoreLib.Interfaces.Contracts
{
    public interface IOwnedImage<TOwner, TOwnerId> : IImage, IOwned<TOwner, TOwnerId>
    {
        public TOwnerId OwnerId { get; set; }
        public TOwner Owner { get; set; }
    }
}