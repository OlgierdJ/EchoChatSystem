using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Interfaces.Contracts;

namespace CoreLib.Entities.EchoCore.ServerCore.GeneralCore.ManagementCore;

public class ServerEventParticipancy : IDomainEntity
{
    public ulong ServerId { get; set; }
    public ulong EventId { get; set; }
    public ulong AccountId { get; set; }
    public DateTime TimeJoined { get; set; } //now
    public Server Server { get; set; } //noaction
    public ServerEvent Event { get; set; } //cascade
    public Account Account { get; set; } //noaction
    public ServerProfile ServerProfile { get; set; } //noaction
}