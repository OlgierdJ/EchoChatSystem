namespace CoreLib.Interfaces.Contracts
{
    public interface IInviteHolder<TInvite>
    {
        public ICollection<TInvite>? Invites { get; set; }

    }

}
