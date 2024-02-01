using CoreLib.Entities.Base;

namespace CoreLib.Entities.EchoCore.AccountCore
{
    public class Friendship : BaseEntity<ulong>
    {
        public ICollection<FriendshipParticipant> FriendshipParticipants { get; set; }
    }
}