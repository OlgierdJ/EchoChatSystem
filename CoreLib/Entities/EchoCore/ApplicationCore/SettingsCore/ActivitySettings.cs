using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities.EchoCore.ApplicationCore.Settings
{
    public class ActivitySettings : BaseEntity<ulong>
    {
        //public ulong AccountSettingsId { get; set; }
        public bool DisplayCurrentActivityAsAStatusMessage { get; set; }
        public bool ShareActivityStatusOnLargeServerJoin { get; set; }
        public bool AllowFriendsToJoinGame { get; set; }
        public bool AllowVoiceChannelParticipantsToJoinGame { get; set; }
        public AccountSettings AccountSettings { get; set; }
    }
}
