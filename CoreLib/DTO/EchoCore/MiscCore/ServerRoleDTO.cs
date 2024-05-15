using CoreLib.Entities.EchoCore.ApplicationCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.MiscCore
{
    public class ServerRoleDTO : RoleDTO
    {
        public string Colour { get; set; } //used for displaying a color beside the role for easy overview.
        public string IconURL { get; set; } //used for displaying an image beside the role if present. //maybe rename or extend the class by a seperate image field
        public bool DisplaySeperatelyFromOnlineMembers { get; set; } //perhaps not needed?
        public bool AllowAnyoneToMention { get; set; } //perhaps not needed?
        public bool IsAdmin { get; set; } //perhaps not needed?
    }
}
