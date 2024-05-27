using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.RequestCore.UserCore
{
    public class RemoveUserConnectionRequestDTO
    {
        //public ulong UserId { get; set; } //get from jwt
        public ulong ConnectionId { get; set; }
    }
}
