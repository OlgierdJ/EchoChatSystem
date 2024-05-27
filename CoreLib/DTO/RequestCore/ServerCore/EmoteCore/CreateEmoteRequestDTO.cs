using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.RequestCore.ServerCore.EmoteCore
{
    public class CreateEmoteRequestDTO
    {
        //public ulong uploaderId {  get; set; } //get from jwt
        //public ulong serverId {  get; set; } //get from route param
        public string ImageFileURL {  get; set; }
        public string Name {  get; set; }
    }
}
