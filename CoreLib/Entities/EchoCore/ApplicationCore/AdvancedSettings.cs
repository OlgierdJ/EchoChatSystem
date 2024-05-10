using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.ApplicationCore
{
    public class AdvancedSettings //: BaseEntity<ulong>
    {
        //public ulong AccountSettingsId { get; set; }
        public bool DeveloperMode { get; set; }
        public bool HardwareAcceleration { get; set; }
        public bool AutoNavigateServerHome { get; set; }
        public AccountSettings AccountSettings { get; set; }
    }
}
