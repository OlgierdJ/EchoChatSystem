using CoreLib.DTO.EchoCore.AccountCore;
using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.FriendCore.UserCore.SettingsCore
{
    public class FriendRequestSettingsDTO
    {
        public ulong Id { get; set; }
        public bool Everyone { get; set; }
        public bool FriendsOfFriends { get; set; }
        public bool ServerMembers { get; set; }
    }
}
