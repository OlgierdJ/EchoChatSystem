using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.ManagementCore;

namespace CoreLib.Entities.EchoCore.ServerCore.ChannelCore
{
    public class ServerTextChannelMemberSettings
    {
        public ulong ChannelId { get; set; } //where these settings belong
        public ulong ProfileId { get; set; } //profile of which these settings affect.


        public ServerTextChannel Channel { get; set; }
        public ServerProfile Profile { get; set; }
        public ICollection<ServerTextChannelMemberPermission>? Permissions { get; set; }
    }
}