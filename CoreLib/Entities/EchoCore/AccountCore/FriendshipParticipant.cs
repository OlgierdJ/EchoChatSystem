using CoreLib.Entities.Base;

namespace CoreLib.Entities.EchoCore.AccountCore
{
    public class FriendshipParticipant : BaseEntity<ulong>
    {
        public Guid AccountId { get; set; }
        public ulong FriendshipId { get; set; }
        public Account Account { get; set; }
        public Friendship Friendship { get; set; }
    }
}