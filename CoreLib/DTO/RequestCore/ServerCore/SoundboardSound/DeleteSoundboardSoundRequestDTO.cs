using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.RequestCore.ServerCore.SoundboardSound
{
    public class DeleteSoundboardSoundRequestDTO
    {
        //public ulong userId { get; set; } //get from jwt
        public ulong SoundId { get; set; }
    }
}
