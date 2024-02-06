using CoreLib.Entities.EchoCore.AccountCore;

namespace CoreLib.Entities.Base
{
    public abstract class BaseParticipancy<TId, TParticipant, TParticipantId, TSubject, TSubjectId> : BaseEntity<TId>
    {
        public TParticipantId ParticipantId { get; set; } //TParticipatorId
        public TSubjectId  SubjectId { get; set; } //TSubject
        public DateTime TimeJoined { get; set; }

        public TParticipant Participant { get; set; }
        public TSubject Subject { get; set; }
    }
}