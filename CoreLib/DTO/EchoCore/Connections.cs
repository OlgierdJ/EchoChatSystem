using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore
{
    public class Connections //: BaseEntity<uint>
    {
        //platform connections like facebook or x or steam
        public Guid AccountId { get; set; }
        public string? PlatformName { get; set; }

        public byte[]? PlatformIcon { get; set; }

        public string? Key_Token { get; set; }

        public string? DisplayName { get; set; }

        public Account? Account { get; set; }
    }
}
