namespace CoreLib.DTO.RequestCore.FriendCore;

public class CancelFriendRequestRequestDTO //used to revoke or decline request
{
    //public ulong SenderId { get; set; } //get from jwt
    public ulong RequestId { get; set; }
}
