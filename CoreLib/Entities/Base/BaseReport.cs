namespace CoreLib.Entities.Base
{
    public abstract class BaseReport<TId> : BaseEntity<TId>
    {
        public string Message { get; set; }
        public DateTime TimeSent { get; set; }
    }
    public abstract class BaseReport<TId, TReporter, TReporterId> : BaseReport<TId>
    {
        public TReporterId ReporterId { get; set; }
        public TReporter Reporter { get; set; }
    }

    public abstract class BaseReport<TId, TReporter, TReporterId, TReason, TReasonId> : BaseReport<TId, TReporter, TReporterId> where TReason : BaseReportReason<TReasonId>
    {
        public ICollection<TReason>? Reasons { get; set; }
    }

    public abstract class BaseReport<TId, TReporter, TReporterId, TReason, TReasonId, TSubject, TSubjectId> : BaseReport<TId, TReporter, TReporterId, TReason, TReasonId> where TReason : BaseReportReason<TReasonId>
    {
        public TSubjectId SubjectId { get; set; }
        public TSubject Subject { get; set; }
    }
}
