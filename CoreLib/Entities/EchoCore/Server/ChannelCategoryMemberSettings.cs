using CoreLib.Entities.Base;

namespace CoreLib.Entities.EchoCore.Server
{
    public class ChannelCategoryMemberSettings : BaseEntity<int>
    {
        public int ChannelCategoryId { get; set; }
        public int RoleId { get; set; }

        public List<ChannelCategoryPermission> ChannelCategoryPermissions { get; set; }
        public ChannelCategory ChannelCategory { get; set; }
        public ServerRole Role { get; set; }
    }
}