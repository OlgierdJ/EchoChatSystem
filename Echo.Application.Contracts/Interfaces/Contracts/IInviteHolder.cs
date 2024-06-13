namespace Echo.Application.Contracts.Interfaces.Contracts;

public interface IInviteHolder<TInvite>
{
    public ICollection<TInvite>? Invites { get; set; }

}
