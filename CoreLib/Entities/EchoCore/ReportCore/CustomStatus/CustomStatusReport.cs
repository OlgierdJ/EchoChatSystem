using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;

namespace CoreLib.Entities.EchoCore.ReportCore.CustomStatus
{
    public class CustomStatusReport : BasePunishableReport<ulong, Account, ulong, CustomStatusReportReason, byte, ReportedCustomStatus, ulong, AccountViolation, ulong>
    {
    }
}
