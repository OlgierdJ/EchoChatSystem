using CoreLib.DTO.EchoCore.AccountCore;
using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.SettingsCore
{
    public class AdvancedSettingsDTO
    {
        public ulong Id { get; set; }
        public bool DeveloperMode { get; set; }
        public bool UseHardwareAccelerationToMakeEchoSmoother { get; set; }
        public bool AutoNavigateServerHome { get; set; }
    }
}
