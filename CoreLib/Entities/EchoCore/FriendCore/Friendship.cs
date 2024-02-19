using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;

namespace CoreLib.Entities.EchoCore.FriendCore
{
    public class Friendship : BaseEntity<ulong>
    {
        public ICollection<Account> Participants { get; set; }
        public DateTime TimeCreated { get; set; }
    }
}