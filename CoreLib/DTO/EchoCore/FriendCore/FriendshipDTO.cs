using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;

namespace CoreLib.DTO.EchoCore.FriendCore
{
    public class FriendshipDTO : BaseEntity<ulong>
    {
        //skal det ik være en list af FriendshipParticipancy
        //public ICollection<Account> Participants { get; set; }
        public DateTime TimeCreated { get; set; }
    }
}