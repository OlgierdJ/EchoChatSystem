using System.ComponentModel.DataAnnotations;

namespace Echo.Application.Contracts.RequestCore.MessageCore;

public class SendMessageRequestDTO //sent to many different controllers such as Textchannel, Chat to create messages within them
{
    //public ulong SenderId { get; set; } get from jwt
    //public ulong HolderId { get; set; } //textchannel or chat that holds this message //get from route
    public string Content { get; set; }
    public ulong? ReplyId { get; set; } //existing message id if this message is a reply.
    public ICollection<SendAttachmentRequestDTO>? Attachments { get; set; }
}

public class SendMessageRequestForm //sent to many different controllers such as Textchannel, Chat to create messages within them
{
    //public ulong SenderId { get; set; } get from jwt
    //public ulong HolderId { get; set; } //textchannel or chat that holds this message //get from route
    [Required]
    [StringLength(1000, ErrorMessage = "message cant be more than 1000 chars")]
    public string Content { get; set; }
    public ulong? ReplyId { get; set; } //existing message id if this message is a reply.
    public ICollection<SendAttachmentRequestDTO>? Attachments { get; set; }
}
