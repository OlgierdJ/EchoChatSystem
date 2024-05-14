using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.FriendCore.RequestCore
{
    public class RemoveFriendRequestDTO //removes friendship
    {
        //public ulong SenderId { get; set; } get from jwt
        public ulong UserId { get; set; }
    }
}
