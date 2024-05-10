using CoreLib.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.ChatCore
{
    public class ChatMessageAttachmentDTO //: BaseMessageAttachment<ulong, ChatMessage, ulong>
    {
        public ulong MessageId { get; set; }
        public string FileURL { get; set; }
    }
}
