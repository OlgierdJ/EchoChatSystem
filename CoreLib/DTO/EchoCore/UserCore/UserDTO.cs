namespace CoreLib.DTO.EchoCore.UserCore
{
    public class UserDTO //this is when a friend is displayed in the friendlist or when you are displayed beside mute and deafen
    {
        public ulong Id { get; set; } //their unique id for mapping interactions to api.
        public string DisplayName { get; set; } //overwritten by accountprofile displayname or nickname if present
        public string Name { get; set; } //unique handle
        public string ImageIconURL { get; set; }
        //public string? Note { get; set; } //this is displayed in their profile.
        public ActiveActivityStatusDTO ActiveStatus { get; set; }
    }
}
