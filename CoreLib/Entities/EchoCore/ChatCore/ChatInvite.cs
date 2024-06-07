using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;

namespace CoreLib.Entities.EchoCore.ChatCore
{
    public class ChatInvite : IInvite<Account, ulong, Chat, ulong>
    {
        public string InviteCode { get; set; }
        public DateTime? ExpirationTime { get; set; }
        public int TimesUsed { get; set; }
        public int TotalUses { get; set; }
        public ulong SubjectId { get; set; }
        public ulong InviterId { get; set; }
        public Account Inviter { get; set; }
        public Chat Subject { get; set; }
    }
}
