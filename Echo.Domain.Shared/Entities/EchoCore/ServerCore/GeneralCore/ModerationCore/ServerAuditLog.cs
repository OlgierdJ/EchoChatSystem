using Echo.Domain.Shared.Entities.Base;
using Echo.Domain.Shared.Entities.EchoCore.AccountCore;
using Echo.Domain.Shared.Entities.EchoCore.ServerCore.GeneralCore;

namespace Echo.Domain.Shared.Entities.EchoCore.ServerCore.GeneralCore.ModerationCore;

public class ServerAuditLog : BaseEntity<ulong>
{
    public ulong AccountId { get; set; }
    public ulong ServerId { get; set; }

    public DateTime TimeLogged { get; set; }
    public string Action { get; set; }

    public Account Account { get; set; }
    public Server Server { get; set; }

}