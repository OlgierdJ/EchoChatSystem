namespace Echo.Application.Contracts.RequestCore.FriendCore;

public class AcceptFriendRequestRequestDTO //used to accept request gaining friendship
{
    //public ulong SenderId { get; set; } //get from jwt
    public ulong RequestId { get; set; }
}
