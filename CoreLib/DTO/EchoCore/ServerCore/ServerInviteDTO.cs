using CoreLib.DTO.EchoCore.UserCore;

namespace CoreLib.DTO.EchoCore.ServerCore
{
    public class ServerInviteDTO //used for displaying and allowing cancellations of server invites.
    {
        public string InviteCode { get; set; }
        public string? Location { get; set; } //where it navigates the user upon joining.
        public int Uses { get; set; } //where it navigates the user upon joining.
        public UserMinimalDTO User { get; set; }
        public DateTime? Expires { get; set; }
    }
}
