using CoreLib.Entities.Base;
using CoreLib.Interfaces;

namespace CoreLib.Entities.EchoCore.AccountCore
{
    public class AccountVideoMute : ITargetedMute<Account, ulong, Account, ulong>, IDomainEntity //mutes the video stream of the other account thus ignoring it
    {
        public ulong SubjectId { get; set; }
        public ulong MuterId { get; set; }
        public DateTime TimeMuted { get; set; }
        public DateTime? ExpirationTime { get; set; }
        public Account Muter { get; set; }
        public Account Subject { get; set; }
    }
}
