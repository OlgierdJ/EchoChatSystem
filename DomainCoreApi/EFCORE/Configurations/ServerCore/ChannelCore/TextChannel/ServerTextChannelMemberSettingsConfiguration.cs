using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.ManagementCore;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.ChannelCore
{
    public class ServerTextChannelMemberSettingsConfiguration
    {
        public ulong ChannelId { get; set; } //where these settings belong
        public ulong ProfileId { get; set; } //profile of which these settings affect.


        public ServerTextChannelConfiguration Channel { get; set; }
        public ServerProfile Profile { get; set; }
        public ICollection<ServerTextChannelMemberPermissionConfiguration>? Permissions { get; set; }
    }
}