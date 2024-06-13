namespace Echo.Application.Contracts.Interfaces.Contracts;

public interface IOwnedChannel<TChannelOwner, TChannelOwnerId> : IChannel, IOwned<TChannelOwner, TChannelOwnerId>
{
}