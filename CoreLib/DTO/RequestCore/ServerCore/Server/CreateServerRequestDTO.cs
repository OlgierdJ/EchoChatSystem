using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.RequestCore.ServerCore.Server
{
    public class CreateServerRequestDTO
    {
        //public ulong UserId { get; set; } //get from jwt
        public string Name { get; set; }
        public bool IsPublic { get; set; }
    }
}
