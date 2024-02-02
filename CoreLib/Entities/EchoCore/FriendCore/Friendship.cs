using CoreLib.Entities.Base;

namespace CoreLib.Entities.EchoCore.FriendCore
{
    public class Friendship : BaseEntity<ulong>
    {
        public ICollection<FriendshipParticipant> FriendshipParticipants { get; set; }
        public DateTime TimeCreated { get; set; }
    }
}