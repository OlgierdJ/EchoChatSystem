using CoreLib.Entities.Enums;

namespace CoreLib.DTO.EchoCore.UserCore.SettingsCore;

public interface IVoiceSettings
{
    bool AdvancedVoiceActivity { get; set; }
    byte Attenuation { get; set; }
    AudioSubSystemMode AudioSubSystemMode { get; set; }
    bool AutomaticallyDetermineInputSensitivity { get; set; }
    bool AutomaticGainControl { get; set; }
    bool DeafenSelf { get; set; }
    bool EchoCancellation { get; set; }
    bool EnableDiagnosticAudioRecording { get; set; }
    bool EnableVoiceDebugLogging { get; set; }
    ulong Id { get; set; }
    string InputDevice { get; set; }
    InputMode InputMode { get; set; }
    byte InputSensitivity { get; set; }
    byte InputVolume { get; set; }
    bool LowerVolumeOfOtherApplicationsWhenISpeak { get; set; }
    bool LowerVolumeOfOtherApplicationsWhenOthersSpeak { get; set; }
    bool MuteSelf { get; set; }
    NoiseSuppression NoiseSuppression { get; set; }
    string OutputDevice { get; set; }
    byte OutputVolume { get; set; }
    bool ShowWarningWhenNoMicInputDetected { get; set; }
}

public class VoiceSettingsDTO : IVoiceSettings
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
