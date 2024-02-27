using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.ReportCore.Profile
{
    public class ReportedCustomStatus : BaseEntity<ulong>
    {
        public ulong AccountId { get; set; }
        public string CustomMessage { get; set; } 
        public Account Account { get; set; }
    }
}
