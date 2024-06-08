using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore.TextChannel;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore;
using CoreLib.Interfaces.Contracts;

namespace CoreLib.Entities.EchoCore.ReportCore.Message
{
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
}
