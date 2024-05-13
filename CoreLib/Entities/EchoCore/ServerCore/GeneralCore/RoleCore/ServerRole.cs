using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore.Category;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.ManagementCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.ServerCore.GeneralCore.RoleCore
{
    public class ServerRole : BaseRole<ulong, ServerProfileServerRole, ServerRolePermission>
    {
        public int Importance { get; set; } //perhaps not needed?
        public string Colour { get; set; } //perhaps not needed?
        public string IconURL { get; set; } //perhaps not needed?
        public bool DisplaySeperatelyFromOnlineMembers { get; set; } //perhaps not needed?
        public bool AllowAnyoneToMention { get; set; } //perhaps not needed?
        public bool IsAdmin { get; set; } //perhaps not needed?



        //these permissions override the global permisssions for this section.
        public ICollection<ServerChannelCategoryRole>? ChannelCategoryRoles { get; set; } //role overrides within channelcategories
        public ICollection<ServerTextChannelRole>? TextChannelRoles { get; set; } //role overrides within textchannels
        public ICollection<ServerVoiceChannelRole>? VoiceChannelRoles { get; set; } //role overrides within voicechannels
        public ICollection<ServerChannelCategoryRolePermission>? ChannelCategoryRolePermissions { get; set; } //permissions allowed within channelcategories, for the role.
        public ICollection<ServerTextChannelRolePermission>? TextChannelRolePermissions { get; set; } //permissions allowed within textchannel, for the role.
        public ICollection<ServerVoiceChannelRolePermission>? VoiceChannelRolePermissions { get; set; } //permissions allowed within voicechannel, for the role.

    }
}
