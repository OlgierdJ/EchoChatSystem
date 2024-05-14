using CoreLib.Entities.EchoCore.ApplicationCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.FriendCore
{
    public class ServerRoleDTO : RoleDTO
    {
        public int Importance { get; set; } //perhaps not needed?
        public string Colour { get; set; } //perhaps not needed?
        public string IconURL { get; set; } //perhaps not needed?
        public bool DisplaySeperatelyFromOnlineMembers { get; set; } //perhaps not needed?
        public bool AllowAnyoneToMention { get; set; } //perhaps not needed?
        public bool IsAdmin { get; set; } //perhaps not needed?
    }
}
