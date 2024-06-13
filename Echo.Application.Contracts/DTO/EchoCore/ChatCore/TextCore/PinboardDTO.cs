using Echo.Application.Contracts.Interfaces.Contracts;

namespace Echo.Application.Contracts.DTO.EchoCore.ChatCore.TextCore;


public class PinboardDTO : IPinboard<MessageDTO>
{
    public ulong Id { get; set; }
    public ICollection<MessageDTO>? PinnedMessages { get; set; }
}
