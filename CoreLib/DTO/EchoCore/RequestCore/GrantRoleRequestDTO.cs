using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.RequestCore
{
    public class GrantRoleRequestDTO // sent to different controllers such as applicationrole, and serverrole to affect roles relative to a user.
    {
        //public ulong SenderId { get; set; } get from jwt
        public ulong UserId { get; set; } //receiving user
        public ulong RoleId { get; set; } //received role
    }
}
