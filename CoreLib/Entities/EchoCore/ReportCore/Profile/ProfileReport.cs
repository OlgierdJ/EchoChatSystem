using CoreLib.DTO.EchoCore.AccountCore;
using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;

namespace CoreLib.Entities.EchoCore.ReportCore.Profile
{
    public class ProfileReport : BasePunishableReport<ulong, Account, ulong, ProfileReportReason, byte, ReportedProfile, ulong, AccountViolation, ulong>
    {
    }
}