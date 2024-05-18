using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.ManagementCore;

namespace CoreLib.Entities.EchoCore.ServerCore.ChannelCore
{
    public class ServerVoiceChannelMemberSettings
    {
        public ulong ChannelId { get; set; } //where these settings belong
        public ulong AccountId { get; set; }
        public ulong ServerId { get; set; }

        public ServerVoiceChannel Channel { get; set; }
        public ServerProfile Profile { get; set; }
        public ICollection<ServerVoiceChannelMemberPermission>? Permissions { get; set; }
    }
}