namespace Echo.Application.Contracts.DTO.EchoCore.UserCore.SettingsCore;

public interface IFriendRequestSettings
{
    bool Everyone { get; set; }
    bool FriendsOfFriends { get; set; }
    ulong Id { get; set; }
    bool ServerMembers { get; set; }
}

public class FriendRequestSettingsDTO : IFriendRequestSettings
{
    public ulong Id { get; set; }
    public bool Everyone { get; set; }
    public bool FriendsOfFriends { get; set; }
    public bool ServerMembers { get; set; }
}
