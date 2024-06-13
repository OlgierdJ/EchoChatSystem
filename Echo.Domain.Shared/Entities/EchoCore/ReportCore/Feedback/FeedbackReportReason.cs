using Echo.Application.Contracts.Interfaces.Contracts;
using Echo.Domain.Shared.Entities.Base;

namespace Echo.Domain.Shared.Entities.EchoCore.ReportCore.Feedback;

public class FeedbackReportReason : BaseEntity<byte>, IReportReason<FeedbackReport>
{
    public ICollection<FeedbackReport>? Reports { get; set; }
    public string Reason { get; set; }
    public string Description { get; set; }
}