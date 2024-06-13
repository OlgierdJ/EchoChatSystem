using Echo.Application.Contracts.Interfaces.Contracts;
using Echo.Domain.Shared.Entities.EchoCore.ServerCore.GeneralCore.ManagementCore;

namespace Echo.Domain.Shared.Entities.EchoCore.ServerCore.GeneralCore.RoleCore;

public class ServerProfileServerRole : IDomainEntity
{
    public ulong AccountId { get; set; }
    public ulong ServerId { get; set; }
    public ulong RoleId { get; set; }
    public DateTime TimeGranted { get; set; }
    public ServerProfile Profile { get; set; }
    public ServerRole Role { get; set; }
}
