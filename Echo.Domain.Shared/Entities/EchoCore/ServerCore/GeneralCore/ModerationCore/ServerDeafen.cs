using Echo.Application.Contracts.Interfaces.Contracts;
using Echo.Domain.Shared.Entities.EchoCore.AccountCore;
using Echo.Domain.Shared.Entities.EchoCore.ServerCore.GeneralCore;

namespace Echo.Domain.Shared.Entities.EchoCore.ServerCore.GeneralCore.ModerationCore;

public class ServerDeafen : ITargetedMute<Server, ulong, Account, ulong>, IDomainEntity //serverwide deafen overrides user deafen setting.
{
    //maybe put bool flag in serverprofile?
    public ulong SubjectId { get; set; }
    public ulong MuterId { get; set; }
    public DateTime TimeMuted { get; set; }
    public DateTime? ExpirationTime { get; set; }
    public Server Muter { get; set; }
    public Account Subject { get; set; }
}
