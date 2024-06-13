using Echo.Application.Contracts.Interfaces.Contracts;
using Echo.Domain.Shared.Entities.Base;
using Echo.Domain.Shared.Entities.EchoCore.AccountCore;

namespace Echo.Domain.Shared.Entities.EchoCore.ReportCore.Bug;

public class BugReport : BaseEntity<ulong>, IReasonedReport<BugReportReason, byte>, IAuthoredReport<Account, ulong>
{
    public ICollection<BugReportReason>? Reasons { get; set; }
    public string Message { get; set; }
    public DateTime TimeSent { get; set; }
    public ulong AuthorId { get; set; }
    public Account Author { get; set; }
}
