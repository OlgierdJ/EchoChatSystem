using CoreLib.Entities.Base;

namespace CoreLib.Entities.EchoCore.AccountCore
{
    public class AccountViolationAppealReview : BaseEntity<ulong>, IViolationAppealReview<AccountViolationAppeal, ulong, Account, ulong>
    {
        public ulong AppealId { get; set; }
        public ulong ReviewerId { get; set; }
        public bool IsDenied { get; set; }
        public AccountViolationAppeal Appeal { get; set; }
        public Account Reviewer { get; set; }
    }
}