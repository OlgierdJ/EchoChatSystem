namespace Echo.Application.Contracts.Interfaces.Contracts;

public interface IReportReason
{
    public string Reason { get; set; }
    public string Description { get; set; }
}

public interface IReportReason<TReport> : IReportReason /*where TReportToReasonEntity : BaseReportReportReason*/
{
    public ICollection<TReport>? Reports { get; set; }
}
