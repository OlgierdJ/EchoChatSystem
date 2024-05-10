using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.ApplicationCore
{
    public class SoundboardSettings //: BaseEntity<ulong>
    {
        public ulong AccountSettingsId { get; set; }
        public byte SoundboardVolume { get; set; } //max 200
        public ulong Soundboard { get; set; } //max 200
        public AccountSettings AccountSettings { get; set; }
    }
}
