using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore;

namespace CoreLib.Entities.EchoCore.ServerCore.GeneralCore.ManagementCore
{
    public class ServerInvite : BaseInvite<Account, ulong, Server, ulong>
    {

        public ulong? ChannelId { get; set; } //if no textchannel is specified the invite still works to the server
        public ServerTextChannel? Channel { get; set; } //if the invite is to a specific textchannel

    }
}
