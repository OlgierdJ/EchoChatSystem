using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.RequestCore
{
    public class AcceptFriendRequestRequestDTO //used to accept request gaining friendship
    {
        //public ulong SenderId { get; set; } //get from jwt
        public ulong RequestId { get; set; }
    }
}
