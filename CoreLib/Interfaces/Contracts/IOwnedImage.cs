namespace CoreLib.Interfaces.Contracts;

public interface IOwnedImage<TOwner, TOwnerId> : IImage, IOwned<TOwner, TOwnerId>
{
}