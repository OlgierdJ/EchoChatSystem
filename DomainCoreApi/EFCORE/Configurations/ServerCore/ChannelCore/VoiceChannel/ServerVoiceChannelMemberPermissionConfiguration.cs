﻿using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.ServerCore.ChannelCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.ManagementCore;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.RoleCore;
using CoreLib.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.ChannelCore
{
    public class ServerVoiceChannelMemberPermissionConfiguration
    //Used for displaying state of serverpermission for a specific channel for a specific member
    {
        //combine foreign key profile and role.
        //pk is combination of channel, role and permission
        public ulong ChannelId { get; set; }
        public ulong ProfileId { get; set; }
        public ulong PermissionId { get; set; }
        public bool? State { get; set; } //true = enabled, null = default, false = disabled
        public ServerVoiceChannelMemberSettingsConfiguration MemberSettings { get; set; } //cascade
        public ServerVoiceChannelConfiguration Channel { get; set; } //ignore
        public ServerProfile Profile { get; set; } //ignore
        public ServerPermission Permission { get; set; } //cascade
    }
}