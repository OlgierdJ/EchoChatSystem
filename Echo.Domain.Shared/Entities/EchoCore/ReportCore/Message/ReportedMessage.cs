﻿using Echo.Application.Contracts.Interfaces.Contracts;
using Echo.Domain.Shared.Entities.Base;
using Echo.Domain.Shared.Entities.EchoCore.AccountCore;

namespace Echo.Domain.Shared.Entities.EchoCore.ReportCore.Message;

public class ReportedMessage : BaseEntity<ulong>, IAuditableMessage, IAuthoredEntity<Account, ulong>
//when a message is reported the unencrypted content of the message, timestamp, and the attachments are to be duplicated into one of these.
//and saved with their own encryption that support-people has access to.
{
    //public DateTime TimeReported { get; set; }
    public string MessageType { get; set; } //idk something like Chat/DM, TextChannel, Other
    public ICollection<ReportedMessageAttachment> Attachments { get; set; }

    public MessageReport Report { get; set; }
    public string Content { get; set; }
    public DateTime TimeSent { get; set; }
    public DateTime? TimeEdited { get; set; }
    public ulong AuthorId { get; set; }
    public Account Author { get; set; }
}