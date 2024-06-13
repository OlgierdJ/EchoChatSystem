using Echo.Application.Contracts.Interfaces.Contracts;
using Echo.Domain.Shared.Entities.Base;

namespace Echo.Domain.Shared.Entities.EchoCore.AccountCore;

public class AccountViolationAppeal : BaseEntity<ulong>, IViolationAppeal<AccountViolation, ulong, AccountViolationAppealReview>
{
    public ulong ViolationId { get; set; }
    public AccountViolation Violation { get; set; }
    public string Message { get; set; }
    public AccountViolationAppealReview? Review { get; set; }
}