using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.RequestCore.ServerCore.EmoteCore
{
    public class EditEmoteRequestDTO
    {
        //public ulong userId {  get; set; } //get from jwt
        //public ulong serverId {  get; set; } //get from route param
        public ulong Id {  get; set; }
        public string Name {  get; set; }
    }
}
