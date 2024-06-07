using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore;
using CoreLib.Interfaces;

namespace CoreLib.Entities.EchoCore.AccountCore
{
    public class AccountServerMute : ITargetedMute<Account, ulong, Server, ulong>, IDomainEntity
    {
        public ulong SubjectId { get; set; }
        public ulong MuterId { get; set; }
        public DateTime TimeMuted { get; set; }
        public DateTime? ExpirationTime { get; set; }
        public Account Muter { get; set; }
        public Server Subject { get; set; }
    }
}
