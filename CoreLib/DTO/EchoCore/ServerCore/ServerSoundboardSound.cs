using CoreLib.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.ServerCore
{
    public class ServerSoundboardSound //: BaseEntity<ulong>
    {
        public ulong ServerId { get; set; }
        public string SoundFileUrl { get; set; }

        public Server Server { get; set; }
    }
}
