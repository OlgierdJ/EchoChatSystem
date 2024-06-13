namespace Echo.Application.Contracts.RequestCore.FriendCore;

public class RemoveFriendRequestDTO //removes friendship
{
    //public ulong SenderId { get; set; } //get from jwt
    public ulong UserId { get; set; }
}
