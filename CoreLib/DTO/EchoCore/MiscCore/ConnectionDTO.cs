using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.MiscCore
{
    public class ConnectionDTO
    {
        public uint Id { get; set; } //connectiontype id
        public string PlatformName { get; set; } //facebook, league of legends, steam, etc.
        public string PlatformIcon { get; set; } //facebook, league of legends, steam, etc.

    }

}
