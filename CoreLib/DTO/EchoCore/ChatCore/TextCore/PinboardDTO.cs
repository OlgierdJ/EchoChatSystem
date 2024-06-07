using CoreLib.DTO.Contracts;

namespace CoreLib.DTO.EchoCore.ChatCore.TextCore
{

    public class PinboardDTO : IPinboard<MessageDTO>
    {
        public ulong Id { get; set; }
        public ICollection<MessageDTO>? PinnedMessages { get; set; }
    }
}
