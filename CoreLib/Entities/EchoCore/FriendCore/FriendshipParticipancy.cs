using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;

namespace CoreLib.Entities.EchoCore.FriendCore
{
    public class FriendshipParticipancy : BaseEntity<ulong>
    {
        public string AccountId { get; set; }
        public ulong FriendshipId { get; set; }
        public Account Account { get; set; }
        public Friendship Friendship { get; set; }
    }
}