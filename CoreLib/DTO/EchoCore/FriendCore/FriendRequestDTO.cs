using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.FriendCore
{
    //måske ikke helt sådan kigger på det kan bare ikke lieg nu
    public class FriendRequestDTO
    {
        public type Request { get; set; }
        public ulong RequestId { get; set; }
        public type Type { get; set; }
        public ulong UserId { get; set; }

    }
    public enum type
    {
        incoming,
        outgoing
    }
}
/*
    incoming / outgoing request
    request id
    type outgoing /incoming
    user profile id
 */
