using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLib.DTO.EchoCore.FriendCore.UserCore.UserCore;

namespace CoreLib.DTO.EchoCore.FriendCore.UserCore.ChatCore
{
    public class VoiceChatDTO
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public ICollection<UserDTO>? ActiveParticipants { get; set; } //null from api, filled by client rtc relation, used for displaying participants
    }
}
