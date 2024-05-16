namespace CoreLib.DTO.EchoCore.ChatCore.TextCore
{
    public class PinboardDTO
    {
        public ulong Id { get; set; }
        public ICollection<MessageDTO>? PinnedMessages { get; set; }
    }
}
