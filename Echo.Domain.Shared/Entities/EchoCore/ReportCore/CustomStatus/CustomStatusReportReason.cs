using Echo.Application.Contracts.Interfaces.Contracts;
using Echo.Domain.Shared.Entities.Base;

namespace Echo.Domain.Shared.Entities.EchoCore.ReportCore.CustomStatus;

public class CustomStatusReportReason : BaseEntity<byte>, IReportReason<CustomStatusReport>
{
    public ICollection<CustomStatusReport>? Reports { get; set; }
    public string Reason { get; set; }
    public string Description { get; set; }
}
