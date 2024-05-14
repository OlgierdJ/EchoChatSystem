using CoreLib.DTO.EchoCore.AccountCore;
using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.Enums;
using CoreLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.FriendCore.UserCore.SettingsCore
{
    public class VoiceSettingsDTO
    {
        public ulong Id { get; set; }
        public string InputDevice { get; set; }
        public string OutputDevice { get; set; }
        public byte InputVolume { get; set; } //max 100
        public byte OutputVolume { get; set; } //max 200
        public InputMode InputMode { get; set; }
        public bool AutomaticallyDetermineInputSensitivity { get; set; }
        public byte InputSensitivity { get; set; }
        public bool EchoCancellation { get; set; }
        public NoiseSuppression NoiseSuppression { get; set; }
        public bool AdvancedVoiceActivity { get; set; }
        public bool AutomaticGainControl { get; set; }

        public byte Attenuation { get; set; } //max 100
        public bool LowerVolumeOfOtherApplicationsWhenISpeak { get; set; }
        public bool LowerVolumeOfOtherApplicationsWhenOthersSpeak { get; set; }
        public AudioSubSystemMode AudioSubSystemMode { get; set; }
        public bool ShowWarningWhenNoMicInputDetected { get; set; }
        public bool EnableDiagnosticAudioRecording { get; set; }
        public bool EnableVoiceDebugLogging { get; set; }

        public bool MuteSelf { get; set; }//dont know if they belong here
        public bool DeafenSelf { get; set; }//dont know if they belong here
    }
}
