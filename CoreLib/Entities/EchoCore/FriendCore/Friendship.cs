using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ChatCore;

namespace CoreLib.Entities.EchoCore.FriendCore
{
    public class Friendship : BaseEntity<ulong>
    {
        public ICollection<FriendshipParticipancy> Participants { get; set; }
        public DateTime TimeCreated { get; set; }
    }
}