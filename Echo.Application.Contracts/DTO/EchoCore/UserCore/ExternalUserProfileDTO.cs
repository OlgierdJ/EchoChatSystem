using Echo.Application.Contracts.DTO.EchoCore.MiscCore;
using Echo.Application.Contracts.DTO.EchoCore.MiscCore.ModerationCore;
using Echo.Application.Contracts.DTO.EchoCore.ServerCore;

namespace Echo.Application.Contracts.DTO.EchoCore.UserCore;

public class ExternalUserProfileDTO //displayed on the server.
{
    public ulong Id { get; set; }
    public UserDTO User { get; set; }
    public ICollection<ServerMinimalDTO>? MutualServers { get; set; } //display always //do not load into users own profile
    public ICollection<UserDTO>? MutualFriends { get; set; } //display always //do not load into users own profile
    //only display if on server. 
    //@everyone is filtered 
    //if displayed with more data such as color, image etc it should be done on client by filtering present roles within the full role list.
    public ICollection<HierarchalRoleMinimalDTO>? Roles { get; set; }
    public ICollection<BadgeDTO>? Badges { get; set; } //only display if on server.
    public ICollection<MembershipBadgeDTO>? MembershipBadges { get; set; } //filled with stuff the user has been a part of. //displays different things depending on where its viewed. within a server or a chat.
    public string BannerColour { get; set; } //overridden by serverprofile if requested on server
    public string? AboutMe { get; set; } //overridden by serverprofile if requested on server
    public string? Note { get; set; }
}
