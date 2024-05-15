using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.RequestCore
{
    public class BlockUserRequestDTO //blocks user
    {
        //public ulong BlockerId { get; set; } //owner from jwt
        public ulong UserId { get; set; } //external user
    }
}
