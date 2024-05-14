using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.FriendCore.RequestCore
{
    public class NicknameUserRequestDTO //adds personal nickname on user
    {
        //public ulong SenderId { get; set; } from jwt
        public ulong UserId { get; set; }
        public string Nickname { get; set; }
    }
}
