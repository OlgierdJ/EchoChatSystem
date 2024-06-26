﻿using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;

namespace CoreLib.Entities.EchoCore.ApplicationCore.Settings
{
    public class WindowSettings : BaseEntity<ulong>
    {
        //public ulong AccountSettingsId { get; set; }
        public bool OpenEchoOnPCStartup { get; set; }
        public bool StartMinimized { get; set; }
        public bool MinimizeOnClose { get; set; }

        public AccountSettings AccountSettings { get; set; }
    }
}
