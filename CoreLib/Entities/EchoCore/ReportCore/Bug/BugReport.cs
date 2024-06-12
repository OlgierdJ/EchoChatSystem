using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Interfaces.Contracts;

namespace CoreLib.Entities.EchoCore.ReportCore.Bug;

public class BugReport : BaseEntity<ulong>, IReasonedReport<BugReportReason, byte>, IAuthoredReport<Account, ulong>
{
    public ICollection<BugReportReason>? Reasons { get; set; }
    public string Message { get; set; }
    public DateTime TimeSent { get; set; }
    public ulong AuthorId { get; set; }
    public Account Author { get; set; }
}
