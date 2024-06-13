namespace Echo.Application.Contracts.DTO.RequestCore.UserCore;

public class RemoveUserConnectionRequestDTO
{
    //public ulong UserId { get; set; } //get from jwt
    public ulong ConnectionId { get; set; }
}
