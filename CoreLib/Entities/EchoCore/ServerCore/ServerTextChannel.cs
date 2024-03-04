using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLib.Entities.Base;

namespace CoreLib.Entities.EchoCore.ServerCore
{
    public class ServerTextChannel : BaseChannel<ulong>
    {
        public bool IsAgeRestricted { get; set; }
        public ICollection<ServerTextChannelAccountMessageTracker> MessageTrackers { get; set; }
    }
}
