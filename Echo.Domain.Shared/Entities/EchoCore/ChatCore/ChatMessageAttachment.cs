using Echo.Application.Contracts.Interfaces.Contracts;
using Echo.Domain.Shared.Entities.Base;

namespace Echo.Domain.Shared.Entities.EchoCore.ChatCore;

public class ChatMessageAttachment : BaseEntity<ulong>, IMessageAttachment<ChatMessage, ulong>
{
    public ulong MessageId { get; set; }
    public string FileLocationURL { get; set; }
    public string FileName { get; set; }
    public string? Description { get; set; }
    public ChatMessage Message { get; set; }
}
