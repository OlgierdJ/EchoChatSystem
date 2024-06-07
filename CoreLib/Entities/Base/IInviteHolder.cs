namespace CoreLib.Entities.Base
{
    public interface IInviteHolder<TInvite>
    {
        public ICollection<TInvite>? Invites { get; set; }

    }

}
