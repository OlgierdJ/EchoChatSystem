using Echo.Application.Contracts.Interfaces.Contracts;
using Echo.Domain.Shared.Entities.Base;
using Echo.Domain.Shared.Entities.EchoCore.AccountCore;

namespace Echo.Domain.Shared.Entities.EchoCore.ReportCore.Profile;

public class ProfileReport : BaseEntity<ulong>, IPunishableReport<ReportedProfile, ulong, AccountViolation, ulong>
{
    public ulong ViolationId { get; set; }
    public AccountViolation? Violation { get; set; }
    public ulong SubjectId { get; set; }
    public ReportedProfile Subject { get; set; }
    public ICollection<ProfileReportReason>? Reasons { get; set; }
    public ulong ReporterId { get; set; }
    public Account Reporter { get; set; }
    public string Message { get; set; }
    public DateTime TimeSent { get; set; }
}