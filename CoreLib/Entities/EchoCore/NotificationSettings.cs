using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore
{
    public class NotificationSettings :BaseEntity<int>
    {
        public ulong AccountId { get; set; }
        public bool DesktopNotification { get; set; }
        public bool UnreadMessageBadge { get; set; }
        public bool TaskbarFlashing { get; set; }
        public Account? Account { get; set; }
    }
}
