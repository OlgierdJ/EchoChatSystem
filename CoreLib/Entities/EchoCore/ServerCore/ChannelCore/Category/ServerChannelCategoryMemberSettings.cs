using CoreLib.Entities.EchoCore.ServerCore.GeneralCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.ManagementCore;

namespace CoreLib.Entities.EchoCore.ServerCore.ChannelCore.Category
{
    public class ServerChannelCategoryMemberSettings
    {
        public ulong ChannelCategoryId { get; set; } //where these settings belong
        
        public ulong AccountId { get; set; }
        public ulong ServerId { get; set; }


        public ServerChannelCategory ChannelCategory { get; set; }
        public ServerProfile Profile { get; set; }
        public Server Server { get; set; } //ignore
        public ICollection<ServerChannelCategoryMemberPermission>? Permissions { get; set; }
    }
}