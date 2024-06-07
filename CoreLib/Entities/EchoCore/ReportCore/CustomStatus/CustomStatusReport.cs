using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;

namespace CoreLib.Entities.EchoCore.ReportCore.CustomStatus
{
    public class CustomStatusReport : BaseEntity<ulong>, IPunishableReport<ReportedCustomStatus, ulong, AccountViolation, ulong>
    {
        public ulong ViolationId { get; set; }
        public AccountViolation? Violation { get; set; }
        public ulong SubjectId { get; set; }
        public ReportedCustomStatus Subject { get; set; }
        public ICollection<CustomStatusReportReason>? Reasons { get; set; }
        public ulong ReporterId { get; set; }
        public Account Reporter { get; set; }
        public string Message { get; set; }
        public DateTime TimeSent { get; set; }
    }
}
