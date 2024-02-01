using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLib.Entities.Base;

namespace CoreLib.Entities.EchoCore.Server
{
    public class ServerTextChannel : BaseChannel
    {
        public bool IsAgeRestricted { get; set; }
    }
}
