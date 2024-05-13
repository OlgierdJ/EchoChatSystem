﻿using CoreLib.Entities.EchoCore.ServerCore.ChannelCore.Category;
using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.RoleCore;

namespace CoreLib.Entities.EchoCore.ServerCore.ChannelCore
{
    public class ServerTextChannelPermission
    //used for mapping displayed permissions within a channelcategory
    {
        //pk is combination of channel, and permission
        public ulong ChannelId { get; set; }
        public ulong PermissionId { get; set; }
        public ServerTextChannel Channel { get; set; } //cascade
        public ServerPermission Permission { get; set; } //cascade
    }
}