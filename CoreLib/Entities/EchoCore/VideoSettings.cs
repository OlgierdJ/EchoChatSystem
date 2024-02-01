using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore
{
    public class VideoSettings : BaseEntity<ulong>
    {
        public ulong AccountSettingsId { get; set; }
        public bool AlwaysPreviewVideo { get; set; }
        public string CameraDevice { get; set; }

        public AccountSettings AccountSettings { get; set; }
    }
}
