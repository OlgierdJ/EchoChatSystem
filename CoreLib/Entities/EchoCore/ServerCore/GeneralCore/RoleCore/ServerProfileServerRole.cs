﻿using CoreLib.Entities.EchoCore.ServerCore.GeneralCore.ManagementCore;

namespace CoreLib.Entities.EchoCore.ServerCore.GeneralCore.RoleCore
{
    public class ServerProfileServerRole
    {
        public ulong AccountId { get; set; }
        public ulong ServerId { get; set; }
        public ulong RoleId { get; set; }
        public DateTime TimeGranted { get; set; }
        public ServerProfile Profile { get; set; }
        public ServerRole Role { get; set; }
    }
}
