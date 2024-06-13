using Echo.Application.Contracts.Interfaces.Contracts;
using Echo.Domain.Shared.Entities.Base;

namespace Echo.Domain.Shared.Entities.EchoCore.AccountCore;

public class AccountViolationAppealReview : BaseEntity<ulong>, IViolationAppealReview<AccountViolationAppeal, ulong, Account, ulong>
{
    public ulong AppealId { get; set; }
    public ulong ReviewerId { get; set; }
    public bool IsDenied { get; set; }
    public AccountViolationAppeal Appeal { get; set; }
    public Account Reviewer { get; set; }
}