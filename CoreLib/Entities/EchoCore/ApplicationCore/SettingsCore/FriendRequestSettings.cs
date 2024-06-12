using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;

namespace CoreLib.Entities.EchoCore.ApplicationCore.Settings;

public class FriendRequestSettings : BaseEntity<ulong>
{
    //public ulong AccountSettingsId { get; set; }
    public AccountSettings AccountSettings { get; set; }
    public bool Everyone { get; set; }
    public bool FriendsOfFriends { get; set; }
    public bool ServerMembers { get; set; }
}
