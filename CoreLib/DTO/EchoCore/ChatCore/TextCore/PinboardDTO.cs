namespace CoreLib.DTO.EchoCore.ChatCore.TextCore
{
    public interface IPinboard<TPinnedMessage>
    {
        ulong Id { get; set; }
        ICollection<TPinnedMessage>? PinnedMessages { get; set; }
    }

    public class PinboardDTO : IPinboard<MessageDTO>
    {
        public ulong Id { get; set; }
        public ICollection<MessageDTO>? PinnedMessages { get; set; }
    }
}
