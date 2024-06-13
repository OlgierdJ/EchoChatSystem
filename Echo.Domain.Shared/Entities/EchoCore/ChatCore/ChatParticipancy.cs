using Echo.Application.Contracts.Interfaces.Contracts;
using Echo.Domain.Shared.Entities.EchoCore.AccountCore;

namespace Echo.Domain.Shared.Entities.EchoCore.ChatCore;

public class ChatParticipancy : IParticipancy<Account, ulong, Chat, ulong>, IDomainEntity
{
    public bool Hidden { get; set; }
    public bool IsOwner { get; set; }
    public ulong ParticipantId { get; set; }
    public ulong SubjectId { get; set; }
    public DateTime TimeJoined { get; set; }
    public Account Participant { get; set; }
    public Chat Subject { get; set; }
}
