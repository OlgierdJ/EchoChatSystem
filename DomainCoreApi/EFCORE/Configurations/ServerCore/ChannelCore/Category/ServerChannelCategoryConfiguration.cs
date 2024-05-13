﻿using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.ChannelCore.Category
{
    public class ServerChannelCategoryConfiguration : BaseEntity<ulong>
    {
        public ulong ServerId { get; set; }
        public int Importance { get; set; } //used for ordering in program
        public string Name { get; set; } //Names can be duplicate but categories will be sorted based on ids
        //public DateTime DateCreated { get; set; } //idk nice to have maybe redundant if we make an audit log
        public bool IsPrivate { get; set; } //privatises the category and all channels within it

        //allowed groups or specific permission????
        public Server Server { get; set; }
        public ICollection<ServerChannelCategoryPermissionConfiguration>? AllowedPermissions { get; set; } //related permissions used for creating subset of all permissions showed to the user
        
        public ICollection<ServerChannelCategoryRoleConfiguration>? AllowedRoles { get; set; } //all roles with specific defined permissions for this category
        public ICollection<ServerChannelCategoryRolePermissionConfiguration>? RolePermissions { get; set; } //all rolepermissions linked to this category
        public ICollection<ServerChannelCategoryMemberSettingsConfiguration>? MemberSettings { get; set; } //member specific definitions for this category
        public ICollection<ServerChannelCategoryMemberPermissionConfiguration>? MemberPermissions { get; set; } //all memberpermissions linked to this category
        //advanced permission settings per role which are enforced upon all channels in the category
        public ICollection<ServerTextChannelConfiguration>? TextChannels { get; set; } //textchannels grouped into this category
        public ICollection<ServerVoiceChannelConfiguration>? VoiceChannels { get; set; } //voicechannels grouped into this category
    }


}
