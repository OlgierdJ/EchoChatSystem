using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Interfaces;

namespace CoreLib.Entities.EchoCore.ServerCore.ChannelCore.TextChannel
{
    public class AccountServerTextChannelMessageTracker : IBaseEntityTracker<Account, ulong, ServerTextChannel, ulong, ServerTextChannelMessage, ulong?>, IDomainEntity
    {
        public ulong OwnerId { get; set; }
        public ulong CoOwnerId { get; set; }
        public ulong? SubjectId { get; set; }
        public Account Owner { get; set; }
        public ServerTextChannel CoOwner { get; set; }
        public ServerTextChannelMessage? Subject { get; set; }
    }
}
