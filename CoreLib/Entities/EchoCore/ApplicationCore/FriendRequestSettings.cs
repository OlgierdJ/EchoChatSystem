using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.ApplicationCore
{
    public class FriendRequestSettings : BaseEntity<ulong>
    {
        //public ulong AccountSettingsId { get; set; }
        public AccountSettings AccountSettings { get; set; }
        //public ulong AccountId { get; set; }
        public bool Everyone { get; set; }
        public bool FriendsOfFriends { get; set; }
        public bool ServerMembers { get; set; }
        //public Account? RequestedAccount { get; set; }
    }
}
