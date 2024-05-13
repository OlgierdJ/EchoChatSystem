﻿using CoreLib.Entities.EchoCore.ServerCore.ChannelCore.Category;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.RoleCore;

namespace DomainCoreApi.EFCORE.Configurations.ServerCore.ChannelCore
{
    public class ServerVoiceChannelPermissionConfiguration
    //used for mapping displayed permissions within a channel
    {
        //pk is combination of channel, and permission
        public ulong ChannelId { get; set; }
        public ulong PermissionId { get; set; }
        public ServerVoiceChannelConfiguration Channel { get; set; } //cascade
        public ServerPermission Permission { get; set; } //cascade
    }
}