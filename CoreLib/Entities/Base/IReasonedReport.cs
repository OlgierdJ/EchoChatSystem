namespace CoreLib.Entities.Base
{
    public  interface IReport
    {
        public string Message { get; set; }
        public DateTime TimeSent { get; set; }
    }

    public interface IReasonedReport<TReason, TReasonId> : IReport where TReason : IReportReason
    {
        public ICollection<TReason>? Reasons { get; set; }
    }
}
