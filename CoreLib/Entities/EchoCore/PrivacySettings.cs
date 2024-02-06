using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore
{
    public class PrivacySettings : BaseEntity<int>
    {
        public ulong AccountId { get; set; }
        public DMAllow DMFromFriends { get; set; }
        public DMAllow DMFromUnknownUsers { get; set; }
        public DMAllow DMFromServerChatroom { get; set; }
        public DMSpamFilter DMSpamFilter { get; set; }
        public Account? Account { get; set; }
    }
}
