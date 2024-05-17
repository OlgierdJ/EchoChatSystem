using CoreLib.DTO.EchoCore.MiscCore.ModerationCore;

namespace CoreLib.DTO.EchoCore.ServerCore
{
    public class ServerRoleDTO : RoleMinimalDTO //used to display a role as they are defined within the server.
    {
        public string Colour { get; set; } //used for displaying a color beside the role for easy overview.
        public string IconURL { get; set; } //used for displaying an image beside the role if present. //maybe rename or extend the class by a seperate image field
        public bool DisplaySeperatelyFromOnlineMembers { get; set; } //perhaps not needed?
        public bool AllowAnyoneToMention { get; set; } //perhaps not needed?
        public bool IsAdmin { get; set; } //perhaps not needed?
    }
}
