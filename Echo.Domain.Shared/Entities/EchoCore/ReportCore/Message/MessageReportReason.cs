using Echo.Application.Contracts.Interfaces.Contracts;
using Echo.Domain.Shared.Entities.Base;

namespace Echo.Domain.Shared.Entities.EchoCore.ReportCore.Message;

public class MessageReportReason : BaseEntity<byte>, IReportReason<MessageReport>
{
    public ICollection<MessageReport>? Reports { get; set; }
    public string Reason { get; set; }
    public string Description { get; set; }
}
