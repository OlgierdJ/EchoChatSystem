using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.RequestCore.ServerCore
{
    public class SetImageRequestDTO
    {
        //public ulong Id { get; set; } //get from jwt
        //public ulong ServerId { get; set; }
        public string ImageFileURL { get; set; } //maybe send image to server fully?
    }
}
