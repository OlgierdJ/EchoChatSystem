using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.RequestCore.ServerCore.EmoteCore
{
    public class DeleteEmoteRequestDTO
    {
        //public ulong userid { get; set; }
        //public ulong ServerId {  get; set; } //get from route param?
        public ulong EmoteId { get; set; }
    }
}
