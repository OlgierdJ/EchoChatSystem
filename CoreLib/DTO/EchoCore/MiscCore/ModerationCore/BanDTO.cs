using CoreLib.DTO.EchoCore.UserCore;

namespace CoreLib.DTO.EchoCore.MiscCore.ModerationCore
{
    public class BanDTO //: BaseEntity<ulong>
    {
        public ulong Id { get; set; } //their unique id for mapping interactions to api.
        //public ulong UserId { get; set; } //just get user from id on serverside when revoking ban
        public string? Reason { get; set; }
        public DateTime? ExpirationTime { get; set; } //null = perma

        public UserMinimalDTO User { get; set; }
        //dont know if we need both name and display name so will just use userminimaldto
        //public string Name { get; set; } //unique handle
        //public string DisplayName { get; set; } //unique handle or displayname if present.
        //public string ImageIconURL { get; set; }
    }
}