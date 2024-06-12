namespace CoreLib.Interfaces.Contracts;

public interface IOwnedChannel<TChannelOwner, TChannelOwnerId> : IChannel, IOwned<TChannelOwner, TChannelOwnerId>
{
}