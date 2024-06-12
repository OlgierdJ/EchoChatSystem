using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.ReportCore.CustomStatus;
using CoreLib.Entities.EchoCore.ReportCore.Message;
using CoreLib.Entities.EchoCore.ReportCore.Profile;
using CoreLib.Interfaces.Contracts;

namespace CoreLib.Entities.EchoCore.AccountCore;

public class AccountViolation : BaseEntity<ulong>, IViolation<ulong, Account, ulong, Account, ulong, AccountViolationAppeal>
{
    public ICollection<CustomStatusReport> ConsumedCustomStatusReports { get; set; }
    public ICollection<MessageReport> ConsumedMessageReports { get; set; }
    public ICollection<ProfileReport> ConsumedProfileReports { get; set; }
    public ulong SubjectId { get; set; }
    public ulong IssuerId { get; set; }
    public int Severity { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public Account Subject { get; set; }
    public Account Issuer { get; set; }
    public AccountViolationAppeal? Appeal { get; set; }
}
