using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.ManagementCore;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.ChannelCore
{
    public class ServerVoiceChannelMemberSettingsConfiguration
    {
        public ulong ChannelId { get; set; } //where these settings belong
        public ulong ProfileId { get; set; } //profile of which these settings affect.


        public ServerVoiceChannelConfiguration Channel { get; set; }
        public ServerProfile Profile { get; set; }
        public ICollection<ServerVoiceChannelMemberPermissionConfiguration>? Permissions { get; set; }
    }
}