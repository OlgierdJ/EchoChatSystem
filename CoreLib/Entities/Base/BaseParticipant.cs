using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.Chat;

namespace CoreLib.Entities.Base
{
    public class BaseParticipant<TMember, TMemberId, TSubject, TSubjectId> : BaseEntity<ulong>
    {
        public TMemberId MemberId { get; set; } //TParticipatorId
        public TSubjectId  SubjectId { get; set; } //TSubject
        public DateTime TimeJoined { get; set; }

        public TMember Member { get; set; }
        public TSubject Subject { get; set; }
    }
}