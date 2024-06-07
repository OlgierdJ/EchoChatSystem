using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;

namespace CoreLib.Entities.EchoCore.ChatCore
{
    public class ChatParticipancy : IParticipancy<Account, ulong, Chat, ulong>
    {
        public bool Hidden { get; set; }
        public bool IsOwner { get; set; }
        public ulong ParticipantId { get; set; }
        public ulong SubjectId { get; set; }
        public DateTime TimeJoined { get; set; }
        public Account Participant { get; set; }
        public Chat Subject { get; set; }
    }
}
