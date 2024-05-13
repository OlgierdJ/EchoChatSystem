using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.ApplicationCore.Settings
{
    public class SoundboardSettings : BaseEntity<ulong>
    {
        //public ulong AccountSettingsId { get; set; }
        public byte SoundboardVolume { get; set; } //max 200
        //public ulong Soundboard { get; set; } //max 200

        /*
         * implement server specific entrance sounds by choosing server and specific soundboard sound.
         * Entrance sound by server and soundboardsound id in the future
         */

        public AccountSettings AccountSettings { get; set; }
    }
}
