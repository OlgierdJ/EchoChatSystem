using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.RequestCore
{
    public class CancelFriendRequestRequestDTO //used to revoke or decline request
    {
        //public ulong SenderId { get; set; } //get from jwt
        public ulong RequestId { get; set; }
    }
}
