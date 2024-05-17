namespace CoreLib.Entities.Base
{
    public abstract class BasePunishableReport<TId, TReporter, TReporterId, TReason, TReasonId, TSubject, TSubjectId, TPunishment, TPunishmentId> : BaseReport<TId, TReporter, TReporterId, TReason, TReasonId, TSubject, TSubjectId> where TReason : BaseReportReason<TReasonId>
    {
        public TPunishmentId? ViolationId { get; set; }
        public TPunishment? Violation { get; set; }
    }
}
