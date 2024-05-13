using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.ManagementCore;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.ChannelCore.Category
{
    public class ServerChannelCategoryMemberSettingsConfiguration
    {
        public ulong ChannelCategoryId { get; set; } //where these settings belong
        public ulong ProfileId { get; set; } //profile of which these settings affect.


        public ServerChannelCategoryConfiguration ChannelCategory { get; set; }
        public ServerProfile Profile { get; set; }
        public ICollection<ServerChannelCategoryMemberPermissionConfiguration>? Permissions { get; set; }
    }
}