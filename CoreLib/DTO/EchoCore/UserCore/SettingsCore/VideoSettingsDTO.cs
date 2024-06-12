namespace CoreLib.DTO.EchoCore.UserCore.SettingsCore;

public interface IVideoSettings
{
    bool AlwaysPreviewVideo { get; set; }
    string CameraDevice { get; set; }
    bool EnableForceQualityOfServicePacketPrio { get; set; }
    bool EnableHardwareAccelerationForVideo { get; set; }
    ulong Id { get; set; }
    bool UseDDLInjectionToCaptureScreen { get; set; }
    bool UseOpenH264VideoCodec { get; set; }
    string VideoBackground { get; set; }
}

public class VideoSettingsDTO : IVideoSettings
{
    public ulong Id { get; set; }
    public bool AlwaysPreviewVideo { get; set; }
    public string CameraDevice { get; set; }
    public string VideoBackground { get; set; }
    public bool UseOpenH264VideoCodec { get; set; }
    public bool EnableHardwareAccelerationForVideo { get; set; }
    public bool EnableForceQualityOfServicePacketPrio { get; set; }
    public bool UseDDLInjectionToCaptureScreen { get; set; }
}
