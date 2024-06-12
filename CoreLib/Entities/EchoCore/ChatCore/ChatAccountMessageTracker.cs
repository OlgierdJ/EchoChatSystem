using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Interfaces.Contracts;

namespace CoreLib.Entities.EchoCore.ChatCore;

public class ChatAccountMessageTracker : IBaseEntityTracker<Account, ulong, Chat, ulong, ChatMessage, ulong?>, IDomainEntity
{
    public ulong OwnerId { get; set; }
    public ulong CoOwnerId { get; set; }
    public ulong? SubjectId { get; set; }
    public Account Owner { get; set; }
    public Chat CoOwner { get; set; }
    public ChatMessage? Subject { get; set; }
}
