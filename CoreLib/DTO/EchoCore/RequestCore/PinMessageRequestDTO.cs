using CoreLib.Entities.EchoCore.ChatCore;
using CoreLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.RequestCore
{
    public class PinMessageRequestDTO //sent to different controllers such as chatpinboard, textchannelpinboard handling the pinnedmessages relative to them.
    {
        //public ulong senderid { get; set; } get from jwt
        public ulong PinboardId { get; set; }
        public ulong MessageId { get; set; }
    }
}
