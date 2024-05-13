﻿using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.ApplicationCore.Settings
{
    public class KeybindSettings : BaseEntity<ulong>
    {
        //public ulong AccountSettingsId { get; set; }
        public ICollection<Keybind>? Keybinds { get; set; }
        public AccountSettings AccountSettings { get; set; }
    }
}