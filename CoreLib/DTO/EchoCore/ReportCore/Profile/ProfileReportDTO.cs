using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;

namespace CoreLib.DTO.EchoCore.ReportCore.Profile
{
    public class ProfileReportDTO //: BasePunishableReport<ulong, Account, ulong, ProfileReportReason, byte, ReportedProfile, ulong, AccountViolation, ulong>
    {
        public ulong AccountId { get; set; }

        public ulong AccountViolationId { get; set; }

        public ulong ProfileReportReasonId { get; set; }
        public ulong ReportedProfileId { get; set; }

        public string Message { get; set; }
        public DateTime TimeSent { get; set; }
    }
}