namespace CoreLib.Entities.Base
{
    public abstract class BaseMessageHolder<TId, TMessage> : BaseEntity<TId>
    {
        public string Name { get; set; }
        public DateTime TimeCreated { get; set; }
        public ICollection<TMessage>? Messages { get; set; }
    }
    public abstract class BaseMessageHolder<TId, TMessage, TParticipant, TPinboard, TInvite, TMute> : BaseMessageHolder<TId, TMessage>
    {
        public TPinboard? Pinboard { get; set; }
        public ICollection<TMute>? Mutes { get; set; }

        public ICollection<TInvite>? Invites { get; set; }

        public ICollection<TParticipant>? Participants { get; set; }
    }

}
