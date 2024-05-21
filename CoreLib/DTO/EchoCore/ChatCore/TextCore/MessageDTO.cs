﻿using CoreLib.DTO.EchoCore.UserCore;

namespace CoreLib.DTO.EchoCore.ChatCore.TextCore
{
    public interface IMessage
    {
        
        string Content { get; set; }
        ulong Id { get; set; }
        MessageDTO? Replied { get; set; }
       
        DateTime? TimeEdited { get; set; }
        DateTime TimeSent { get; set; }
    }

    public interface IRepliableMessage<TReply> : IMessage
    {
        public TReply? Replied { get; set; } //message parent if present.
    }

    public interface IMessageWithAttachments<TMessageAttachment> : IMessage
    {
        ICollection<TMessageAttachment>? Attachments { get; set; }
    }

    public interface IMessageWithSender<TSender> : IMessage
    {
        TSender? Sender { get; set; }
    }

    public class MessageDTO : IMessage, 
        IMessageWithSender<UserMinimalDTO>,
        IMessageWithAttachments<MessageAttachmentDTO>, 
        IRepliableMessage<MessageDTO>
    {
        public MessageDTO? Replied { get; set; } //message parent if present.
        //public ulong SenderId { get; set; } //id of original message sender //get from userminimaldto
        public UserMinimalDTO? Sender { get; set; } //id of original message sender
        public ulong Id { get; set; } //id of original message
                                             //(this id refers to the relative id in whatever store the message is stored in.
                                             //on client this is determined by the holder of this object. example: TextChannelMessageDTO)
        public string Content { get; set; } //message text, emotes, formatting, etc.
        public DateTime TimeSent { get; set; } //time message has been sent
        public DateTime? TimeEdited { get; set; } //if message has been edited display edited time at last line of content
        public ICollection<MessageAttachmentDTO>? Attachments { get; set; }
    }
}
