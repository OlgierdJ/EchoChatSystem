using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.RequestCore.MessageCore
{
    public class SendMessageRequestDTO //sent to many different controllers such as Textchannel, Chat to create messages within them
    {
        //public ulong SenderId { get; set; } get from jwt
        public ulong HolderId { get; set; } //textchannel or chat that holds this message
        public string Content { get; set; }
        public ulong? ReplyId { get; set; } //existing message id if this message is a reply.
        public ICollection<SendAttachmentRequestDTO>? Attachments { get; set; }
    }
}
