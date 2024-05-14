using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.FriendCore.RequestCore
{
    public class ServerKickUserRequestDTO //kicks user from server
    {
        //public ulong adminid { get; set; } //from jwt
        public ulong UserId { get; set; }
        public string? Reason { get; set; } // for audit log
    }
}
