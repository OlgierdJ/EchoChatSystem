using CoreLib.Entities.EchoCore.AccountCore;

namespace CoreLib.Entities.Base
{
    public abstract class BaseParticipant<TId, TMember, TMemberId, TSubject, TSubjectId> : BaseEntity<TId>
    {
        public TMemberId MemberId { get; set; } //TParticipatorId
        public TSubjectId  SubjectId { get; set; } //TSubject
        public DateTime TimeJoined { get; set; }

        public TMember Member { get; set; }
        public TSubject Subject { get; set; }
    }
}