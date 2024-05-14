using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.FriendCore.UserCore.UserCore
{
    public class UserProfileDTO
    {
        public ulong Id { get; set; }
        public UserDTO User { get; set; }
        public ICollection<ServerDTO>? MutualServers { get; set; } //display always
        public ICollection<UserDTO>? MutualFriends { get; set; } //display always
        public ICollection<ServerRoleDTO>? Roles { get; set; } //only display if on server.
        public DateTime MemberSince { get; set; }
        public DateTime? ServerMemberSince { get; set; }
        public string BannerColour { get; set; }
        public string? AboutMe { get; set; }
        public string? Note { get; set; }
    }
}
