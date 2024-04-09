using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.Enums;
using CoreLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.ApplicationCore
{
    public class VoiceSettings : BaseEntity<ulong>
    {
        public ulong AccountSettingsId { get; set; }
        public string InputDevice { get; set; }
        public string OutputDevice { get; set; }
        public byte InputVolume { get; set; } //max 200
        public byte OutputVolume { get; set; } //max 200
        public InputMode InputMode { get; set; }
        public byte InputSensitivity { get; set; }
        public bool EchoCancellation { get; set; }
        public NoiseSuppression NoiseSuppression { get; set; }
        public bool AdvancedVoiceActivity { get; set; }
        public bool AutomaticGainControl { get; set; }

        public AccountSettings AccountSettings { get; set; }
    }
}
