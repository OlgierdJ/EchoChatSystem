using CoreLib.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.AccountCore
{
    public class AccountSession : BaseEntity<long>
    {
        public string SessionToken { get; set; }
        public DateTime TimeStarted { get; set; }
        public DateTime? ExpirationTime { get; set; }
        public DateTime? TimeStopped { get; set; }
    }
}
