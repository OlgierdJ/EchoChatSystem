using Echo.Application.Contracts.Interfaces.Contracts;
using Echo.Domain.Shared.Entities.Base;
using Echo.Domain.Shared.Entities.EchoCore.ReportCore.CustomStatus;
using Echo.Domain.Shared.Entities.EchoCore.ReportCore.Message;
using Echo.Domain.Shared.Entities.EchoCore.ReportCore.Profile;

namespace Echo.Domain.Shared.Entities.EchoCore.AccountCore;

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
