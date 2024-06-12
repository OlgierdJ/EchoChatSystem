using CoreLib.Entities.Base;
using CoreLib.Interfaces.Contracts;

namespace CoreLib.Entities.EchoCore.ReportCore.Message;

public class ReportedMessageAttachment : BaseEntity<ulong>, IMessageAttachment<ReportedMessage, ulong>
{
    public ulong MessageId { get; set; }
    public string FileLocationURL { get; set; }
    public string FileName { get; set; }
    public string? Description { get; set; }
    public ReportedMessage Message { get; set; }
}