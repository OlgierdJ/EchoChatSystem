namespace CoreLib.Entities.Base
{
    public interface ITargetedMute<TMuter, TMuterId, TSubject, TSubjectId>
    {
        public TSubjectId SubjectId { get; set; }
        public TMuterId MuterId { get; set; }
        public DateTime TimeMuted { get; set; }
        public DateTime? ExpirationTime { get; set; } //null = permanent
        public TMuter Muter { get; set; }
        public TSubject Subject { get; set; }
    }
}
