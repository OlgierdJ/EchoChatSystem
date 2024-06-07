using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore;

namespace CoreLib.Entities.EchoCore.ServerCore.ChannelCore.TextChannel
{
    public class AccountServerTextChannelMute : ITargetedMute<Account, ulong, ServerTextChannel, ulong>
    {
        public ulong SubjectId { get; set; }
        public ulong MuterId { get; set; }
        public DateTime TimeMuted { get; set; }
        public DateTime? ExpirationTime { get; set; }
        public Account Muter { get; set; }
        public ServerTextChannel Subject { get; set; }
    }
}
