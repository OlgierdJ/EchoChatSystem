namespace CoreLib.Interfaces.Contracts
{
    public interface ITargetedReport<TSubject, TSubjectId> : IReport
    {
        public TSubjectId SubjectId { get; set; }
        public TSubject Subject { get; set; }
    }
}
