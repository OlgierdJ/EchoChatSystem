using Echo.Domain.Shared.Entities.Base;
using Echo.Domain.Shared.Entities.EchoCore.AccountCore;

namespace Echo.Domain.Shared.Entities.EchoCore.ApplicationCore.SettingsCore;

public class VideoSettings : BaseEntity<ulong>
{
    //public ulong AccountSettingsId { get; set; }
    public bool AlwaysPreviewVideo { get; set; }
    public string CameraDevice { get; set; }
    public string VideoBackground { get; set; }
    public bool UseOpenH264VideoCodec { get; set; }
    public bool EnableHardwareAccelerationForVideo { get; set; }
    public bool EnableForceQualityOfServicePacketPrio { get; set; }
    public bool UseDDLInjectionToCaptureScreen { get; set; }

    public AccountSettings AccountSettings { get; set; }
}
