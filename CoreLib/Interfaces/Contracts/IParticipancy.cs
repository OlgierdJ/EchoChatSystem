namespace CoreLib.Interfaces.Contracts
{
    public interface IParticipancy<TParticipant, TParticipantId, TSubject, TSubjectId>
    {
        public TParticipantId ParticipantId { get; set; } //TParticipatorId
        public TSubjectId SubjectId { get; set; } //TSubject
        public DateTime TimeJoined { get; set; }

        public TParticipant Participant { get; set; }
        public TSubject Subject { get; set; }
    }
}