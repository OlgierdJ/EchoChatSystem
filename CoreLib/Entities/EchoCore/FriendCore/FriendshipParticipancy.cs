using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;

namespace CoreLib.Entities.EchoCore.FriendCore
{
    public class FriendshipParticipancy : IParticipancy<Account, ulong, Friendship, ulong>/*: BaseEntity<ulong>*/
    {
        public ulong ParticipantId { get; set; }
        public ulong SubjectId { get; set; }
        public DateTime TimeJoined { get; set; }
        public Account Participant { get; set; }
        public Friendship Subject { get; set; }
    }
}