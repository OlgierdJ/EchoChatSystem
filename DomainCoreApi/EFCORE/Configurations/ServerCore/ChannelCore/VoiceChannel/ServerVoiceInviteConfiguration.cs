using CoreLib.DTO.EchoCore.AccountCore;
using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.ChannelCore.VoiceChannel
{
    public class ServerVoiceInviteConfiguration : BaseInvite<Account, ulong, Server, ulong>
    {
        //maybe extend to allow "guest link" for voicechannel which basically works as the user is allowed into only that specific channel but once they leave they are kicked from the server
        public ulong ChannelId { get; set; } //must have a voicechannel
        public bool GuestInvite { get; set; } //auto-kick when leaving voicecall (wont be implemented at first but maybe later)
        public ServerVoiceChannelConfiguration Channel { get; set; } //if the invite is to a specific voicechannel
        
    }
}
