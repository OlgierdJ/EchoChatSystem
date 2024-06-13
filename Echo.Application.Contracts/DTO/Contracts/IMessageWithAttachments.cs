namespace Echo.Application.Contracts.DTO.Contracts;

public interface IMessageWithAttachments<TMessageAttachment> : IMessage
{
    ICollection<TMessageAttachment>? Attachments { get; set; }
}
