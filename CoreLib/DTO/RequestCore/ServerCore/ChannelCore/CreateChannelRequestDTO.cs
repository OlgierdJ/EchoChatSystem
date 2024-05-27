using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.RequestCore.ServerCore.ChannelCore
{
    public class CreateChannelRequestDTO
    {
        //public ulong userId { get; set; } //getjwt
        //public ulong serverId { get; set; } //get route
        public string Name { get; set; }
        public bool IsPrivate { get; set; }
    }
}
