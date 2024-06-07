namespace CoreLib.Entities.Base
{
    public interface ITargetedReport<TSubject, TSubjectId> : IReport
    {
        public TSubjectId SubjectId { get; set; }
        public TSubject Subject { get; set; }
    }
}
