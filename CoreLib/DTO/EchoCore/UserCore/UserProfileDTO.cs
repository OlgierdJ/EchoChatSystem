using CoreLib.DTO.EchoCore.MiscCore;
using CoreLib.DTO.EchoCore.ServerCore;

namespace CoreLib.DTO.EchoCore.UserCore
{
    public class UserProfileDTO //used for displaying ones own accountprofiles and serverprofiles alike.
    {
        public ulong Id { get; set; }
        public UserDTO User { get; set; }
        public ICollection<BadgeDTO>? Badges { get; set; } //only display if on server.
        //public DateTime MemberSince { get; set; } //echo member
        //public DateTime? ServerMemberSince { get; set; } //overridden by serverprofile if requested on server
        public string BannerColour { get; set; } //overridden by serverprofile if requested on server
        public string? AboutMe { get; set; } //overridden by serverprofile if requested on server
        public string? Note { get; set; }
    }
}
