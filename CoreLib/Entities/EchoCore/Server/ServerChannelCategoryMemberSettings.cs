using CoreLib.Entities.Base;

namespace CoreLib.Entities.EchoCore.Server
{
    public class ServerChannelCategoryMemberSettings : BaseEntity<int>
    {
        public int ChannelCategoryId { get; set; }
        public int RoleId { get; set; }

        public List<ServerChannelCategoryPermission> ChannelCategoryPermissions { get; set; }
        public ChannelCategory ChannelCategory { get; set; }
        public ServerRole Role { get; set; }
    }
}