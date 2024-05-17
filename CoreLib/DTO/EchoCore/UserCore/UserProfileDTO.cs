using CoreLib.DTO.EchoCore.ServerCore;

namespace CoreLib.DTO.EchoCore.UserCore
{
    public class UserProfileDTO
    {
        public ulong Id { get; set; }
        public UserDTO User { get; set; }
        public ICollection<ServerDTO>? MutualServers { get; set; } //display always //do not load into users own profile
        public ICollection<UserDTO>? MutualFriends { get; set; } //display always //do not load into users own profile
        public ICollection<ServerRoleDTO>? Roles { get; set; } //only display if on server.
        public DateTime MemberSince { get; set; }
        public DateTime? ServerMemberSince { get; set; } //overridden by serverprofile if requested on server
        public string BannerColour { get; set; } //overridden by serverprofile if requested on server
        public string? AboutMe { get; set; } //overridden by serverprofile if requested on server
        public string? Note { get; set; }
    }
}
