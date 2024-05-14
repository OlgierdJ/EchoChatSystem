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
    public class SoundboardSettingsDTO
    {
        public ulong Id { get; set; }
        public byte SoundboardVolume { get; set; } //max 200
        //public ulong Soundboard { get; set; } //max 200

        /*
         * implement server specific entrance sounds by choosing server and specific soundboard sound.
         * Entrance sound by server and soundboardsound id in the future
         */

    }
}
