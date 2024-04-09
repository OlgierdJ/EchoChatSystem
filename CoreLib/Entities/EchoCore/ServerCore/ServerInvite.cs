using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.ServerCore
{
    public class ServerInvite : BaseInvite<ulong,Account, string, Server, string>
    {
        //maybe extend to allow "guest link" for voicechannel which basically works as the user is allowed into only that specific channel but once they leave they are kicked from the server
        public ulong? ChannelId { get; set; }
        public ServerTextChannel? Channel { get; set; } //if the invite is to a specific textchannel
        //or should it be in the baseinvite?\\
        public ulong ServerId { get; set; }
        public Server Server { get; set; }
    }
}
