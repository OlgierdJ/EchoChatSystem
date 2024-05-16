namespace CoreLib.DTO.EchoCore.UserCore.SettingsCore
{
    public class FriendRequestSettingsDTO
    {
        public ulong Id { get; set; }
        public bool Everyone { get; set; }
        public bool FriendsOfFriends { get; set; }
        public bool ServerMembers { get; set; }
    }
}
