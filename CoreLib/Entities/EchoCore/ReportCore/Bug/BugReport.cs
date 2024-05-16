using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;

namespace CoreLib.Entities.EchoCore.ReportCore.Bug
{
    public class BugReport : BaseReport<ulong, Account, ulong, BugReportReason, byte>
    {
    }
}
