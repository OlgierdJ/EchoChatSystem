using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.RequestCore.ServerCore.Event
{
    public class JoinEventRequestDTO
    {
        //public ulong Id {  get; set; } //get from jwt
        //public ulong serverId { get; set; } //server from route param
        public ulong EventId {  get; set; } 
    }
}
