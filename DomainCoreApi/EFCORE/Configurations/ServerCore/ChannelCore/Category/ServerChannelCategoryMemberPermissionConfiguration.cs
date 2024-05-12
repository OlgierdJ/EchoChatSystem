using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.ManagementCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.RoleCore;
using CoreLib.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.ChannelCore.Category
{
    public class ServerChannelCategoryMemberPermissionConfiguration
    //Used for displaying state of serverpermission for a specific channel for a specific role
    {
        //combine foreign key category and role.
        //pk is combination of category, role and permission
        public ulong ChannelCategoryId { get; set; } //where it affects
        public ulong ProfileId { get; set; } //affected
        public ulong PermissionId { get; set; } //given permission
        public bool? State { get; set; } //state. (true = enabled, null = default, false = disabled)
        public ServerChannelCategoryMemberSettingsConfiguration MemberSettings { get; set; } //cascade
        public ServerChannelCategoryConfiguration ChannelCategory { get; set; } //ignore
        public ServerProfile Profile { get; set; } //ignore
        public ServerPermission Permission { get; set; } //cascade

    }
}
