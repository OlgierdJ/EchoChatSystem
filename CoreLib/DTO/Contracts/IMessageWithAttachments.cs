namespace CoreLib.DTO.Contracts
{
    public interface IMessageWithAttachments<TMessageAttachment> : IMessage
    {
        ICollection<TMessageAttachment>? Attachments { get; set; }
    }
}
