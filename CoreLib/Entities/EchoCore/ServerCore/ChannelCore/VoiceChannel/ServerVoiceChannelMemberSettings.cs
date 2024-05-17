using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.ManagementCore;

namespace CoreLib.Entities.EchoCore.ServerCore.ChannelCore
{
    public class ServerVoiceChannelMemberSettings
    {
        public ulong ChannelId { get; set; } //where these settings belong
        public ulong ProfileId { get; set; } //profile of which these settings affect.


        public ServerVoiceChannel Channel { get; set; }
        public ServerProfile Profile { get; set; }
        public ICollection<ServerVoiceChannelMemberPermission>? Permissions { get; set; }
    }
}