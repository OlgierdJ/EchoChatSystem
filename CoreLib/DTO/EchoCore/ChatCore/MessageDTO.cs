using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.ChatCore
{
    public class MessageDTO
    {
        public MessageDTO? Replied { get; set; } //message parent if present.
        public ulong SenderId { get; set; } //id of original message sender
        public ulong MessageId { get; set; } //id of original message
                                             //(this id refers to the relative id in whatever store the message is stored in.
                                             //on client this is determined by the holder of this object. example: TextChannelMessageDTO)
        public string Content { get; set; } //message text, emotes, formatting, etc.
        public DateTime TimeSent { get; set; } //time message has been sent
        public DateTime? TimeEdited { get; set; } //if message has been edited display edited time at last line of content
        public ICollection<MessageAttachmentDTO>? Attachments { get; set; }
    }
}
