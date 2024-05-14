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
    public class VideoSettingsDTO
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
}
