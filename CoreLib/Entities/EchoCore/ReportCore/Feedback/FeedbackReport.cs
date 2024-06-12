using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Interfaces.Contracts;

namespace CoreLib.Entities.EchoCore.ReportCore.Feedback;

public class FeedbackReport : BaseEntity<ulong>, IReasonedReport<FeedbackReportReason, byte>
{
    public ICollection<FeedbackReportReason>? Reasons { get; set; }
    public ulong ReporterId { get; set; }
    public Account Reporter { get; set; }
    public string Message { get; set; }
    public DateTime TimeSent { get; set; }
}
