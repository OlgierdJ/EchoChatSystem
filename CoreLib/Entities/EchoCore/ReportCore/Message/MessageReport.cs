using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;

namespace CoreLib.Entities.EchoCore.ReportCore.Message
{
    public class MessageReport : BasePunishableReport<ulong, Account, ulong, MessageReportReason, byte, ReportedMessage, ulong, AccountViolation, ulong>
    {
    }
}
