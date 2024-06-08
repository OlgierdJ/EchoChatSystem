namespace CoreLib.Interfaces.Contracts
{

    public interface IReasonedReport<TReason, TReasonId> : IReport where TReason : IReportReason
    {
        public ICollection<TReason>? Reasons { get; set; }
    }
}
