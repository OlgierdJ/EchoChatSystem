using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.FriendCore.RequestCore
{
    public class ServerBanUserRequestDTO //bans user from server
    {
        //public ulong adminid { get; set; } //from jwt
        public ulong UserId { get; set; }
        public string? Reason { get; set; } //why are they banned
        public DateTime? TimeExpired { get; set; } //null = perma //until when are they banned
        public DateTime TimeKeepMessagesBefore { get; set; } //used to determine if and how many messages to delete upon ban.
    }
}
