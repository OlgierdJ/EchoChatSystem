using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.ManagementCore;

namespace CoreLib.Entities.EchoCore.ServerCore.ChannelCore.Category
{
    public class ServerChannelCategoryMemberSettings
    {
        public ulong ChannelCategoryId { get; set; } //where these settings belong
        public ulong ProfileId { get; set; } //profile of which these settings affect.


        public ServerChannelCategory ChannelCategory { get; set; }
        public ServerProfile Profile { get; set; }
        public ICollection<ServerChannelCategoryMemberPermission>? Permissions { get; set; }
    }
}