using CoreLib.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.ServerCore
{
    public class ServerVoiceChannel //: BaseChannel<ulong>
    {
        public ulong ChannelCategoryId { get; set; }
        public ServerChannelCategory ChannelCategory { get; set; }
    }
}
