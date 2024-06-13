using Echo.Application.Contracts.Interfaces.Contracts;
using Echo.Domain.Shared.Entities.Base;

namespace Echo.Domain.Shared.Entities.EchoCore.ReportCore.Bug;

public class BugReportReason : BaseEntity<byte>, IReportReason<BugReport>
{
    public ICollection<BugReport>? Reports { get; set; }
    public string Reason { get; set; }
    public string Description { get; set; }
}
