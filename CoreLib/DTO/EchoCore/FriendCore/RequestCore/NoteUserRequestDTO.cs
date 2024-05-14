using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.FriendCore.RequestCore
{
    public class NoteUserRequestDTO //adds personal note to user
    {
        //public ulong SenderId { get; set; } from jwt
        public ulong UserId { get; set; }
        public string Note { get; set; }
    }
}
