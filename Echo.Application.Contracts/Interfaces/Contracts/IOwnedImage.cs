namespace Echo.Application.Contracts.Interfaces.Contracts;

public interface IOwnedImage<TOwner, TOwnerId> : IImage, IOwned<TOwner, TOwnerId>
{
}