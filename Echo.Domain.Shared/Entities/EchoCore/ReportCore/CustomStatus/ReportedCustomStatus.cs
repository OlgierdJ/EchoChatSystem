using Echo.Domain.Shared.Entities.Base;
using Echo.Domain.Shared.Entities.EchoCore.AccountCore;

namespace Echo.Domain.Shared.Entities.EchoCore.ReportCore.CustomStatus;

public class ReportedCustomStatus : BaseEntity<ulong>
{
    public ulong AccountId { get; set; }
    public string CustomMessage { get; set; }
    public Account Account { get; set; }
    public CustomStatusReport Report { get; set; }
}
