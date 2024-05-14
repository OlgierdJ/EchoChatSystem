using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.ChatCore
{
    public class PinboardDTO
    {
        public ulong Id { get; set; }
        public ICollection<MessageDTO>? PinnedMessages { get; set; }
    }
}
