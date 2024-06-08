namespace CoreLib.Interfaces.Contracts
{
    public interface IOwnedChannel<TChannelOwner, TChannelOwnerId> : IChannel, IOwned<TChannelOwner, TChannelOwnerId>
    {
        public TChannelOwnerId? OwnerId { get; set; }
        public TChannelOwner? Owner { get; set; }
    }
}