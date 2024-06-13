namespace Echo.Application.Contracts.Interfaces.Contracts;

public interface IOwnedPinboard<TMessagePin, TOwner, TOwnerId> : IPinboard<TMessagePin>
{
    public TOwnerId OwnerId { get; set; }
    public TOwner Owner { get; set; }
}
