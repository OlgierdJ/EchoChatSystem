using Echo.Application.Contracts.Interfaces.Contracts;
using Echo.Domain.Shared.Entities.EchoCore.AccountCore;

namespace Echo.Domain.Shared.Entities.EchoCore.ChatCore;

public class ChatInvite : IInvite<Account, ulong, Chat, ulong>, IDomainEntity
{
    public string InviteCode { get; set; }
    public DateTime? ExpirationTime { get; set; }
    public int TimesUsed { get; set; }
    public int TotalUses { get; set; }
    public ulong SubjectId { get; set; }
    public ulong InviterId { get; set; }
    public Account Inviter { get; set; }
    public Chat Subject { get; set; }
}
