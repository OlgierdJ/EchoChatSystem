using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.ApplicationCore
{
    public class NotificationSettings //: BaseEntity<ulong>
    {
        //her lave det om til at NotificationSettings kigger på AccountSettings i stedefor a kigge Account
        //public ulong AccountSettingsId { get; set; }
        public bool FocusModeEnabled { get; set; } //for not receiving in app sounds
        public bool DesktopNotification { get; set; }
        public bool UnreadMessageBadge { get; set; }
        public bool TaskbarFlashing { get; set; }
        public AccountSettings AccountSettings { get; set; }
    }
}
