using CoreLib.Interfaces.Contracts;

namespace CoreLib.Entities.EchoCore.AccountCore
{
    public class AccountMute : ITargetedMute<Account, ulong, Account, ulong>, IDomainEntity
    {
        public ulong SubjectId { get; set; }
        public ulong MuterId { get; set; }
        public DateTime TimeMuted { get; set; }
        public DateTime? ExpirationTime { get; set; }
        public Account Muter { get; set; }
        public Account Subject { get; set; }
    }
}
