using CoreLib.Interfaces;

namespace CoreLib.Entities.Base
{
    public interface IBaseMessageHolder<TId, TMessage> : IEntity<TId>
    {
        public string Name { get; set; }
        public DateTime TimeCreated { get; set; }
        public ICollection<TMessage>? Messages { get; set; }
    }
    public interface IBaseMessageHolder<TId, TMessage, TParticipant, /*TPinboard,*/ TInvite, TMute> : IBaseMessageHolder<TId, TMessage>
    {
        //public TPinboard? Pinboard { get; set; }
        public ICollection<TMute>? Mutes { get; set; }


        public ICollection<TParticipant>? Participants { get; set; }
    }

    public interface IInviteHolder<TInvite>
    {
        public ICollection<TInvite>? Invites { get; set; }

    }

}
