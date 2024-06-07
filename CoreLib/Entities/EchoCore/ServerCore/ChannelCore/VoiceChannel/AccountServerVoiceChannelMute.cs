using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore;

namespace CoreLib.Entities.EchoCore.ServerCore.ChannelCore.VoiceChannel
{
    public class AccountServerVoiceChannelMute : ITargetedMute<Account, ulong, ServerVoiceChannel, ulong>
    {
        public ulong SubjectId { get; set; }
        public ulong MuterId { get; set; }
        public DateTime TimeMuted { get; set; }
        public DateTime? ExpirationTime { get; set; }
        public Account Muter { get; set; }
        public ServerVoiceChannel Subject { get; set; }
    }
}
