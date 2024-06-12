using CoreLib.Entities.Enums;

namespace CoreLib.DTO.EchoCore.UserCore;

public class FriendRequestDTO
{
    public ulong Id { get; set; } //requestId
    public UserDTO Person { get; set; } //inner friend containing how they are displayed, id to interact with.
    public RequestType Type { get; set; } //enable / disable accept, decline / revoke functionality based on this


    //public ulong AccountId { get; set; } //who to accept
    //public string DisplayName { get; set; } //displayname if present else shows handle both here and name.
    //public string Name { get; set; } //users handle (hidden until hover).
    //public string BadgeImageIconURL { get; set; }
    //public AccountProfileDTO Profile { get; set; } //dont know if this should be present.

}
