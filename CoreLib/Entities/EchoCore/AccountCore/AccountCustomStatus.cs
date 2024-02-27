using CoreLib.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.AccountCore
{
    public class AccountCustomStatus : BaseEntity<ulong>
    {
        public string CustomMessage { get; set; } //this will show instead of your actual activity even if the actual activity status message has been disabled in settings but will not show if invisible
        public DateTime? ExpirationTime { get; set; } //null = permanent
        public Account Account { get; set; }
    }
}
