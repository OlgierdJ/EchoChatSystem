using CoreLib.DTO.EchoCore.AccountCore;
using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.ApplicationCore.Settings
{
    public class StreamerModeSettings : BaseEntity<ulong>
    {
        //public ulong AccountSettingsId { get; set; }

        public bool EnableStreamerMode { get; set; }
        public bool AutomaticallyEnableAndDisableIfStreaming { get; set; }
        public bool HidePersonalInformation { get; set; }
        public bool HideInviteLinks { get; set; }
        public bool DisableSounds { get; set; }
        public bool DisableNotifications { get; set; }
        public bool HideEchoWindowFromScreenCapture { get; set; }

        public AccountSettings AccountSettings { get; set; }
    }
}
